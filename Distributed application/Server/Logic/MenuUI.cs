using Server.Connection;
using Server.Services;
using System.ComponentModel;

namespace Server.Logic
{
    public class MenuUI
    {
        private List<string> _generalCommands = new List<string> {"Help","Login" , "Item management" };
        private List<string> _localCommands = new List<string> { "Help", "Display users", "User management", };
        private List<string> _userCommands = new List<string> { "Back", "Show user", "Show all", "Add", "Update", "Delete" };
        private List<string> _itemCommands = new List<string> { "Add" };

        private new LoginManagementLogic _loginLogic;
        private new LocalLogic _localLogic;
        private new ItemsManagementLogic _itemsManagementLogic;

        public void ServerOptions(string data)
        {
            //Receive full data spaced by ' '
            //create a string that takes the command name and pass it Find... and it remove the first thing
            //take the command param from the string and pass the rest of the data to the appropiate case where it will be further proccesed by the function

            string[] parts = data.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;

            string command = parts[0];
            string restOfData = string.Join(" ", parts, 1, parts.Length - 1);

            int index = FindCommandIndex(_generalCommands, command);

            if (index == -1)
            {
                switch (index)
                {
                    case 0:
                        _loginLogic.Login(data);
                        break;
                }
            }
        }
        public void LocalOptions(string command)
        {

            int index = FindCommandIndex(_localCommands, command);

            if (index != -1)
            {
                switch (index)
                {
                    case 0:
                        Help();
                        break;
                    case 1:
                        _localLogic.DisplayConnectedUsers();
                        break;
                    case 2:
                        UserManagement();
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
        protected void ItemManagement(string data)
        {

            Console.WriteLine("Please chose one of the following commands: ");
            for (int i = 0; i < _itemCommands.Count; i++) 
            {
                Console.WriteLine($"{i + 1}. {_itemCommands[i]}");
            }

            string userInput = Console.ReadLine();
            int index = FindCommandIndex( _itemCommands, userInput);
            if (index != -1)
            {
                switch (index)
                {
                    case 0:
                        _itemsManagementLogic.AddItem(data);
                        break;
                }
            }

        }
        protected void UserManagement()
        {
            
            Console.WriteLine("Please chose one of the following commands: ");
            for (int i = 0; i < _userCommands.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_userCommands[i]}");
            }

            string userInput = Console.ReadLine();
            int index = FindCommandIndex(_userCommands, userInput);
            if (index != -1)
            {
                switch (index)
                {
                    case 0:
                        return;
                    case 1:
                        _localLogic.GetUser();
                        break;
                    case 2:
                        _localLogic.GetAllUsers();
                        break;
                    case 3:
                        _localLogic.AddUser();
                        break;
                    case 4:
                        _localLogic.UpdateUser();
                        break;
                    case 5:
                        _localLogic.DeleteUser();
                        break;

                }
            } 
        }

        private int FindCommandIndex(List<string> commands, string input)
        {
            if (int.TryParse(input, out int number) && number > 0 && number <= commands.Count) return number -1;

            return commands.FindIndex(cmd => cmd.StartsWith(input, StringComparison.OrdinalIgnoreCase));
        }
    }
}
