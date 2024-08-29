using Server.Connection;
using Server.Services;

namespace Server.Menu
{
    public class MenuUI : Logic
    {
        private List<string> _generalCommands = new List<string> {"Help", "Display users", "User management", "Item management" };
        private List<string> _userCommands = new List<string> { "Back", "Show user", "Show all", "Add", "Update", "Delete" };
        private List<string> _itemCommands = new List<string> { "Add" };

        public MenuUI(IUserService userService, IItemService itemService) : base(userService, itemService)
        {
        }

        public void Options(string command)
        {

            int index = FindCommandIndex(_generalCommands, command);

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
                    case 3:
                        ItemManagement();
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
        protected void ItemManagement()
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
                        AddItem();
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
                        GetUser();
                        break;
                    case 2:
                        GetAllUsers();
                        break;
                    case 3:
                        AddUser();
                        break;
                    case 4:
                        UpdateUser();
                        break;
                    case 5:
                        DeleteUser();
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
