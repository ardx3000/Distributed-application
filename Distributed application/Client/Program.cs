using Client.Connection;
using System.Diagnostics;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SocketClient socketClient = new SocketClient();

            socketClient.Connect("127.0.0.1", 9999);

            socketClient.Send("Hello from client! ");

            string response = socketClient.Received();
            Debug.WriteLine("Server: " + response);

            socketClient.Close();
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}