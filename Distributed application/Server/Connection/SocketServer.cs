using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Server.Connection
{
    public class SocketServer
    {
        private Socket _listener;
        private Thread _listenerThread;
        private bool _isRunning = false;

        public SocketServer(int port)
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(new IPEndPoint(IPAddress.Any, port));
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
                Debug.WriteLine("Client Connected: " + clientSocket.RemoteEndPoint.ToString());

                //Adding each client to a thread
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(clientSocket);
            }
        }

        private void HandleClient(object clientSocketObj)
        {
            Socket clientSocket = (Socket)clientSocketObj;
            byte[] buffer = new byte[1024];

            while (_isRunning)
            {
                int bytesReceived;
                try
                {
                    bytesReceived = clientSocket.Receive(buffer);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        Debug.WriteLine("Client disconnected abruptly.");
                    }
                    else
                    {
                        Debug.WriteLine("SocketException: " + ex.Message);
                    }
                    break;
                }

                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                Debug.WriteLine("Received from client: " + dataReceived);

                string response = "Server Received: " + dataReceived;
                byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
                clientSocket.Send(responseBuffer);
            }

            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
