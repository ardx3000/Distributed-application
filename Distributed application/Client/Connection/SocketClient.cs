using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace Client.Connection
{
    class SocketClient
    {
        private Socket _socketClient;

        public SocketClient()
        {
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        
        public void Connect(string ipAddress, int port)
        {
            IPAddress serverIP = IPAddress.Parse(ipAddress);
            IPEndPoint remoteEP = new IPEndPoint(serverIP, port);

            try
            {
                _socketClient.Connect(remoteEP);
                Debug.WriteLine("Connected to {0}", _socketClient.RemoteEndPoint.ToString());
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Connection failed: " +  ex.Message);
            }
        }

        public void Send(string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            _socketClient.Send(byteData);
        }

        public string Received()
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = _socketClient.Receive(buffer);
            return Encoding.ASCII.GetString(buffer, 0, bytesReceived);
        }
        public void Close()
        {
            _socketClient.Shutdown(SocketShutdown.Both);
            _socketClient.Close();
        }
    }
}
