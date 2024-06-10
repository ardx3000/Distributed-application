using Server.Connection;
using System;

namespace Server.Menu
{
    public class MenuUI
    {
        private List<string> _commands = new List<string> {"Help", "Display users", "User management"};
        private SocketServer _socketServerInstance; // Instance of SocketServer

        public void Options(string command)
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

        private void Help()
        {
            int i = 0;
            foreach (string command in _commands)
            {
                i++;
                Console.WriteLine($"{i}.{command},");
            }
            
        }

        private void DisplayConnectedUsers()
        {
            SocketServer.DisplayConnectedClients();
        }

        private void UserManagement()
        {
            //TODO call ADD USER => Create the functions that interact with db
            //TODO call DELETE USER => Create the functions that interact with db
        }

        //TODO add a way to interactr with the db
    }
}
