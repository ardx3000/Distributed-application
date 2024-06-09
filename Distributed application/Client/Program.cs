using Client.Connection;
using Client.Menu;
using System;

namespace Client
{
    class Program
    {
    

        static void Main(string[] args)
        {
            // SENSITIVE DATA ONLY HARDCODED FOR DEMONSTRATION.
            byte[] key = { 0xd5, 0xa3, 0xd0, 0xc4, 0xcf, 0x72, 0xff, 0x6d, 0x64, 0xd1, 0xb8, 0xfd, 0x62, 0x4d, 0xc1, 0x43 };
            byte[] iv = { 0xb0, 0xa1, 0xc2, 0xd3, 0xe4, 0xf5, 0x67, 0x78, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00 };
            //-----------------------------------------------------------------------------------------------------------------
   

            SocketClient socketClient = new SocketClient(key, iv);
            socketClient.DataReceived += SocketClient_DataReceived;
            
            Console.WriteLine("Connecting to server...");
            socketClient.Connect("127.0.0.1", 9999);

            MenuUI menu = new MenuUI(socketClient);
            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "Exit") break;

                menu.Options(userInput);
            }            
        }

        private static void SocketClient_DataReceived(object sender, string data)
        {
            Console.WriteLine($"Data received: {data}");
        }
    }
}