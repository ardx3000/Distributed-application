using Server.Connection;
using System.Diagnostics;

namespace Server
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            SocketServer server = new SocketServer(9999);
            server.Start();
            Debug.WriteLine("Server is starting and listening to connections....");


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}