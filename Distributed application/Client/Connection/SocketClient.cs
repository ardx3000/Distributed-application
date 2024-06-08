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
        private bool _connected = false;
        private int _maxRetries = 3;
        private TimeSpan _retryInterval = TimeSpan.FromSeconds(5);

       // public event EventHandler<string> DataReceived;

        public SocketClient(byte[] key, byte[] iv)
        {
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _encryption = new AESEncryption(key, iv);
        }
        
        public void Connect(string ipAddress, int port)
        {
            int retries = 0;
            
            while (!_connected && retries < _maxRetries)
            {
                try
                {
                    IPAddress serverIP = IPAddress.Parse(ipAddress);
                    IPEndPoint remoteEP = new IPEndPoint(serverIP, port);
                    _socketClient.Connect(remoteEP);
                    _connected = true;
                }
                catch (FormatException ex)
                {
                    Debug.WriteLine("Invalid IP address fromat: " + ipAddress);
                    break;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"Connection attempt failed: {ex.Message}");
                    retries++;
                    Debug.WriteLine($"Retrying connection... (Attempt {retries}/{_maxRetries})");

                    // Exponential backoff: Increase the retry interval exponentially
                    Thread.Sleep(_retryInterval);
                    _retryInterval = TimeSpan.FromSeconds(_retryInterval.TotalSeconds * 2);
                }
            }


            if (!_connected)
            {
                Debug.WriteLine("Field to connect after max retries! ");
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


        }
        /*
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
        */
        public void Close()
        {
            _socketClient.Shutdown(SocketShutdown.Both);
            _socketClient.Close();
        }
        /*
        protected virtual void OnDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }*/
    }
}
