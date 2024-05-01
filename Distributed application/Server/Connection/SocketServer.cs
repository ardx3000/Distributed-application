﻿using System;
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
        private AESEncryption _encryption;

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

                    Debug.WriteLine("(SERVER) Received from client (encrypted): " + receivedData);
                    Debug.WriteLine("(SERVER) Received from client (decrypted): " + decryptedData);


                    // Process the decrypted data as needed
                    // Example: echo back the decrypted message
                    byte[] responseData = _encryption.EncryptString("(SERVER) Server Received: " + decryptedData);
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
    }
}
