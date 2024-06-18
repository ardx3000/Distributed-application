using Server.Connection;

namespace Server.Menu
{
    public class MenuUI : MenuLogic
    {
        private List<string> _generalCommands = new List<string> {"Help", "Display users", "User management"};
        private List<string> _userCommands = new List<string> { "Back", "Show user", "Show all", "Add", "Update", "Delete" };
        
        public void Options(string command)
        {

            int index = _generalCommands.IndexOf(command);

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
                Console.WriteLine($"Invalid command use {_generalCommands.ElementAt(0)} !");
            }
        }

        public void Help()
        {
            int i = 0;
            foreach (string command in _generalCommands)
            {
                i++;
                Console.WriteLine($"{i}.{command},");
            }
            
        }
        protected void UserManagement()
        {
            int i = 0;
            Console.WriteLine("Please chose one of the following commands: ");
            foreach (string command in _userCommands)
            {

                i++;
                Console.WriteLine($"{i}.{command},");
            }

            string userInput = Console.ReadLine();

            int index = _userCommands.IndexOf(userInput);
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
    }
}
