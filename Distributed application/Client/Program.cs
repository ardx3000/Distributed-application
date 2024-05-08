using Client.Connection;
using System.Diagnostics;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        //TODO Finish the linking of the data received from the server to the ui
        [STAThread]
        static void Main()
        {
            // SENSITIVE DATA ONLY HARDCODED FOR DEMOSTRATION.
            byte[] key = { 0xd5, 0xa3, 0xd0, 0xc4, 0xcf, 0x72, 0xff, 0x6d, 0x64, 0xd1, 0xb8, 0xfd, 0x62, 0x4d, 0xc1, 0x43 };
            byte[] iv = { 0xb0, 0xa1, 0xc2, 0xd3, 0xe4, 0xf5, 0x67, 0x78, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00 };
            //-----------------------------------------------------------------------------------------------------------------

            SocketClient socketClient = new SocketClient(key, iv);

            socketClient.Connect("127.0.0.1", 9999);

            socketClient.Send("(CLIENT) Hello from client! ");

            string response = socketClient.Received();
            Debug.WriteLine("(CLIENT) Server: " + response);

            socketClient.Close();
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}