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
        //TODO Fix the problem where the client does not receive data from the server , posible cause that the client starts before the server and it does not have a recconection mechanism.
        [STAThread]
        static void Main()
        {
            // SENSITIVE DATA ONLY HARDCODED FOR DEMOSTRATION.
            byte[] key = { 0xd5, 0xa3, 0xd0, 0xc4, 0xcf, 0x72, 0xff, 0x6d, 0x64, 0xd1, 0xb8, 0xfd, 0x62, 0x4d, 0xc1, 0x43 };
            byte[] iv = { 0xb0, 0xa1, 0xc2, 0xd3, 0xe4, 0xf5, 0x67, 0x78, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00 };
            //-----------------------------------------------------------------------------------------------------------------

            SocketClient socketClient = new SocketClient(key, iv);

            // socketClient.Connect("127.0.0.1", 9999);

            // socketClient.Send("(CLIENT) Hello from client! ");
            
            Thread connectThread = new Thread(() =>
            {
                socketClient.Connect("127.0.0.1", 9999);

            });
            connectThread.Start();
            //Posible threading problem
            Thread receiveThread = new Thread(() =>
            {
                socketClient.ReceiveData();
                Debug.WriteLine("(ON CLIENT)" + socketClient.ReceiveData);
            });
            receiveThread.Start();
            //string response = socketClient.ReceiveData();
            //Debug.WriteLine("(CLIENT) Server: " + response);

            //socketClient.Close();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 mainForm = new Form1(socketClient);
            Application.Run(mainForm);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();

        }
    }
}