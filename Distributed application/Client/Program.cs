using Client.Connection;
using Client.Menu;
using System;
using Microsoft.Extensions.DependencyInjection;
using Client.Services;

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


            // Set up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton(new SocketClient(key, iv))
                .AddSingleton<LoginService>()
                .AddSingleton<MenuUI>()
                .BuildServiceProvider();

            var socketClient = serviceProvider.GetRequiredService<SocketClient>();


            //SocketClient socketClient = new SocketClient(key, iv);
            socketClient.DataReceived += SocketClient_DataReceived;
            
            Console.WriteLine("Connecting to server...");
            socketClient.Connect("127.0.0.1", 9999);
            Console.WriteLine("Connected to server");
           // MenuUI menu = new MenuUI(socketClient);
           var menuUI = serviceProvider.GetRequiredService<MenuUI>();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Please use one of the follwoing commands");
            menuUI.Help();

            while (true)
            {
                Console.WriteLine("-------------------------------------------------------------");
                string userInput = Console.ReadLine();

                if (userInput == "Exit") break;

                menuUI.Options(userInput);
            }            
        }

        private static void SocketClient_DataReceived(object sender, string data)
        {
            Console.WriteLine($"Data received: {data}");
        }
    }
}