using Client.Connection;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class LoginService
    {
        private string _sessionTokken;
        private readonly SocketClient _socketClient;
        private bool _isLoginResponseReceived;
        private string _loginResponse;
        private readonly object _lock = new object(); 

        public LoginService(SocketClient socketClient)
        {
            _socketClient = socketClient;
            _socketClient.DataReceived += OnDataReceived;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(_sessionTokken);

        public bool Login(string username, string password)
        {

            // Hash the password
            string hashedPassword = HashingUtility.HashString(password);

            // Sending credintials to the server
            _socketClient.Send($"LOGIN {username} {hashedPassword}");

            int timeout = 5000;
            int elapsedTime = 0;
            int waitInterval = 100;


            while (!_isLoginResponseReceived && elapsedTime < timeout)
            {
                Thread.Sleep(waitInterval);
                elapsedTime += waitInterval;
                //wait for a response for n secounds 
            }

            if (!_isLoginResponseReceived)
            {
                Console.WriteLine("Login timed out.");
                return false;
            }
            lock (_lock)
            {
                if (!string.IsNullOrEmpty(_loginResponse) && _loginResponse.StartsWith("LOGIN_SUCCESS"))
                {
                    _sessionTokken = _loginResponse.Split(' ')[1];
                    return true;
                }
                Console.WriteLine("Login failed!");
                return false;
            }
        }

        public void Logout()
        {
            if (IsLoggedIn)
            {
                _socketClient.Send($"LOGOUT {_sessionTokken}");
                _sessionTokken = null;
            }
        }

        public bool Authentiticate(string command)
        {
            if (!IsLoggedIn)
            {
                Console.WriteLine("You need to login...");
                return false;
            }
            return true;
        }

        private void OnDataReceived(object sender, string data)
        {
            _loginResponse = data;
            //Create a condition to check the data
            _isLoginResponseReceived = true;
        }
    }
}
