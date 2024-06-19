using Client.Connection;
using System;
using System.ComponentModel.Design;

namespace Client.Menu
{
    public class MenuUI
    {
        private List<string> _commands = new List<string> { "Help", "Test" };
        private SocketClient _socketClient;

        public MenuUI(SocketClient socketClient)
        {
            _socketClient = socketClient;
        }

        public  void Options(string command)
        {
            int index = _commands.IndexOf(command);

            if (index != -1)
            {
                switch(index)
                {
                    case 0:
                        Help();
                        break;
                    case 1:
                        testSend();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Invalid command use {_commands.ElementAt(0)} !");
            }
        }

        private  void Help()
        {
            foreach (string command in _commands)
            {
                Console.WriteLine($"{command},");
            }
        }
        private void testSend()
        {
            Console.WriteLine("Enter the massage to be sent: ");
            string data =  Console.ReadLine();
            _socketClient.Send(data);
        }
    }
}
