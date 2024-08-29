using Client.Connection;
using System;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace Client.Menu
{
    public class MenuUI : MenuLogic
    {
        private List<string> _generalCommands = new List<string> { "Help", "Test", "Add data" };
        private SocketClient _socketClient;

        public MenuUI(SocketClient socketClient) : base(socketClient)
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
                        Help();
                        break;
                    case 1:
                        TestSend();
                        break;
                    case 2:
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
