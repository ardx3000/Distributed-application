using Client.Connection;
using Client.Services;
using System;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace Client.Menu
{
    public class MenuUI : MenuLogic
    {
        private List<string> _generalCommands = new List<string> { "Login", "Help", "Test", "Add data" };
        private SocketClient _socketClient;

        public MenuUI(SocketClient socketClient, LoginService loginService) : base(socketClient, loginService)
        {
        }

        public void Options(string command)
        {
            int index = FindCommandIndex(_generalCommands, command);

            if (index != -1)
            {
                switch(index)
                {
                    case 0:
                        Login();
                        break;
                    case 1:
                        Help();
                        break;
                    case 2:
                        TestSend();
                        break;
                    case 3:
                        AddData();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Invalid command use {_generalCommands.ElementAt(0)} !");
            }
        }

        public void Help()
        {
            for (int i = 0; i < _generalCommands.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_generalCommands[i]}");
            }
        }



        private int FindCommandIndex(List<string> commands, string input)
        {
            if (int.TryParse(input, out int number) && number > 0 && number <= commands.Count) return number - 1;

            return commands.FindIndex(cmd => cmd.StartsWith(input, StringComparison.OrdinalIgnoreCase));
        }
        //TODO create a function where the user can login 


    }
}
