using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace Server.Connection
{
    public class SocketServer
    {
        private Socket _listener;
        private Thread _listenerThread;
        private bool _isRunning = false;
        private AESEncryption _encryption;
        private static List<Socket> _connectedClients = new List<Socket>(); // list with all the connected clients

        public event EventHandler<string> DataReceived;


        //Add a send method to send data to the cleint -_- forgot
        public SocketServer(int port, byte[] key, byte[] iv)
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(new IPEndPoint(IPAddress.Any, port));
            _encryption = new AESEncryption(key, iv);
        }

        public void Start()
        {
            _listener.Listen(10);
            _isRunning = true;

            _listenerThread = new Thread(ListenForClients);
            _listenerThread.Start();
        }

        public void Stop()
        {
            _isRunning = false;
            _listener.Close();
        }

        private void ListenForClients()
        {
            while (_isRunning)
            {
                //Client connecting
                Socket clientSocket = _listener.Accept();
                Debug.WriteLine("(SERVER) Client Connected: " + clientSocket.RemoteEndPoint.ToString());

                //Add the newly connected client to the list
                _connectedClients.Add(clientSocket);

                //Adding each client to a thread
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(clientSocket);
            }
        }

        private void HandleClient(object clientSocketObj)
        {
            Socket clientSocket = (Socket)clientSocketObj;
            byte[] buffer = new byte[1024];

            try
            {
                string notification = "(From SERVER) new client connected: " + clientSocket.RemoteEndPoint.ToString();
                byte[] notificationData = Encoding.ASCII.GetBytes(notification);
                foreach (Socket connectedClient in _connectedClients)
                {
                    connectedClient.Send(notificationData);
                }
                while (_isRunning)
                {
                    int bytesReceived = clientSocket.Receive(buffer);
                    if (bytesReceived == 0)
                        break;

                    byte[] receivedData = new byte[bytesReceived];
                    Array.Copy(buffer, receivedData, bytesReceived);

                    // Decode received data from Base64
                    string receivedBase64 = Encoding.ASCII.GetString(receivedData);
                    byte[] encryptedData = Convert.FromBase64String(receivedBase64);
                    ;
                    // Decrypt received data
                    string decryptedData = _encryption.DecryptBytes(encryptedData);

                    //Add timestamp
                    string timestamp = DateTime.Now.ToString("[HH:mm:ss]");
                    string messageWithTimestamp = $"{timestamp}: {decryptedData}";

                    //Triger the DataReceived event with the decrypt data
                    OnDataReceived(decryptedData);

                    // Process the decrypted data as needed
                    // Example: echo back the decrypted message
                    byte[] responseData = _encryption.EncryptString("(From SERVER On Client) Server Received: " + decryptedData);
                    string encryptedResponse = Convert.ToBase64String(responseData);
                    clientSocket.Send(Encoding.ASCII.GetBytes(encryptedResponse));
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("(SERVER) SocketException: " + ex.Message);
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                Debug.WriteLine("(SERVER) Client Disconnected.");
            }
        }
        public void SendToClient(Socket clientSocket, string message)
        {
            byte[] responseData = _encryption.EncryptString(message);
            string encryptedResponse = Convert.ToBase64String(responseData);
            byte[] byteData = Encoding.ASCII.GetBytes(encryptedResponse);

            try
            {
                clientSocket.Send(byteData);
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("(SERVER) SocketException while sending to client: " + ex.Message);
            }
        }
        public static void DisplayConnectedClients()
        {
            try
            {
                if (_connectedClients.Count == 0)
                {
                    Console.WriteLine("No users connected.");
                }
                else
                {
                    foreach (Socket connection in _connectedClients)
                    {
                        try
                        {
                            // Attempt to access the RemoteEndPoint property
                            Console.WriteLine($"{connection.RemoteEndPoint}");
                        }
                        catch (ObjectDisposedException)
                        {
                            Console.WriteLine("A connection was disposed.");
                        }
                        catch (SocketException ex)
                        {
                            Console.WriteLine($"Socket error: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while displaying connected clients: {ex.Message}");
            }
        }

        protected virtual void OnDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }
    }
}
