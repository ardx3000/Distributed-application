using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using client.Connection;

namespace Client.Connection
{
    public class SocketClient
    {
        private Socket _socketClient;
        private AESEncryption _encryption;

        public event EventHandler<string> DataReceived;

        public SocketClient(byte[] key, byte[] iv)
        {
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _encryption = new AESEncryption(key, iv);
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
            Debug.WriteLine("(CLIENT): data: " + data);
            byte[] encryptedData = _encryption.EncryptString(data); // Encrypt the data
            string encodedData = Convert.ToBase64String(encryptedData); // Convert the encrypted data to a string
            Debug.WriteLine("(CLIENT): encrypted + encoded data: " + encodedData);
            byte[] byteData = Encoding.ASCII.GetBytes(encodedData); // Convert the string to byte array
            _socketClient.Send(byteData); // Send the encrypted and encoded data
        }
/*
        public string Received()
        {
            try
            {
                if (!_socketClient.Connected)
                {
                    Debug.WriteLine("Sock is not connected! ");
                    return null;


                }
                byte[] buffer = new byte[1024];
                int bytesReceived = _socketClient.Receive(buffer);
                string encodedResponse = Encoding.ASCII.GetString(buffer, 0, bytesReceived); // Convert the received bytes to a string
                byte[] encryptedResponse = Convert.FromBase64String(encodedResponse); // Convert the string to byte array
                return _encryption.DecryptBytes(encryptedResponse); // Decrypt the response

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error receving data " + ex.Message);
                return null;
            }


        }*/

        //modified to not block calls
       public void ReceiveData()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesReceived = _socketClient.Receive(buffer);
                    string encodedResponse = Encoding.ASCII.GetString(buffer, 0, bytesReceived); // Convert the received bytes to a string
                    byte[] encryptedResponse = Convert.FromBase64String(encodedResponse); // Convert the string to byte array
                    string decryptedResponse = _encryption.DecryptBytes(encryptedResponse); // Decrypt the response

                    // Raise the DataReceived event with the decrypted response
                    OnDataReceived(decryptedResponse);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error receiving data:" + ex.Message);
            }
        }

        public void Close()
        {
            _socketClient.Shutdown(SocketShutdown.Both);
            _socketClient.Close();
        }

        protected virtual void OnDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }
    }
}
