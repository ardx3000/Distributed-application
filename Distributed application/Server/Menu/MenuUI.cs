using Server.Connection;
using System;

namespace Server.Menu
{
    public static class MenuUI
    {
        private static List<string> _commands = new List<string> {"Help", "Display users", "User management"};
        private static SocketServer _socketServerInstance; // Instance of SocketServer

        public static void Options(string command)
        {
            int index = _commands.IndexOf(command);

            if (index != -1)
            {
                switch (index)
                {
                    case 0:
                        Help();
                        break;
                    case 1:
                        DisplayConnectedUsers();
                        break;
                    case 2:
                        UserManagement();
                        break;

                }

            }
            else
            {
                Console.WriteLine($"Invalid command use {_commands.ElementAt(0)} !");
            }
        }

        private static void Help()
        {
            foreach (string command in _commands)
            {
                Console.WriteLine($"{command},");
            }
            
        }

        private static void DisplayConnectedUsers()
        {
            SocketServer.DisplayConnectedClients();
        }

        private static void UserManagement() { }
    }
}
