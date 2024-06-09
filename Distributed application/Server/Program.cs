using Server.Connection;
using Server.Menu;

namespace Server
{
    class Program
    {
        private static MenuUI _menu = new MenuUI();

        static void Main()
        {

            // SENSITIVE DATA ONLY HARDCODED FOR DEMOSTRATION.
            byte[] key = { 0xd5, 0xa3, 0xd0, 0xc4, 0xcf, 0x72, 0xff, 0x6d, 0x64, 0xd1, 0xb8, 0xfd, 0x62, 0x4d, 0xc1, 0x43 };
            byte[] iv = { 0xb0, 0xa1, 0xc2, 0xd3, 0xe4, 0xf5, 0x67, 0x78, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00 };
            //-----------------------------------------------------------------------------------------------------------------

            SocketServer socketServer = new SocketServer(9999, key, iv);
            socketServer.DataReceived += Server_DataReceived;
            socketServer.Start();
            Console.WriteLine("(SERVER) Server is starting and listening to connections....");
            while (true)
            {
                Console.WriteLine("-------------------------------------------------------------");
                string userInput = Console.ReadLine();
                _menu.Options(userInput);
            }

       
        }

        private static void Server_DataReceived(object sender, string data)
        {
            Console.WriteLine($"Data received: {data}");
        }
    }
}