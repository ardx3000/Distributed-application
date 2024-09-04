
using Client.Connection;
using Client.Services;

namespace Client.Menu
{
    public class MenuLogic 
    {
        protected SocketClient _socketClient; // Protected field to store the socket client
        protected readonly LoginService _loginService;

        public MenuLogic(SocketClient socketClient, LoginService loginService)
        {
            _socketClient = socketClient; // Initialize the socket client
            _loginService = loginService;
        }



        protected void Login()
        {
            if (_loginService.IsLoggedIn)
            {
                Console.WriteLine("You are already logged in use Help command");
                return;
            }

            Console.WriteLine("Enter your username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            bool success = _loginService.Login(username, password);
            if (success)
            {
                Console.WriteLine("Login successfull please use Help command to se the available options ! ");
            }
            else
            {
                Console.WriteLine("Login failed. POlease try again ! ");
            }
        }

        protected void TestSend()
        {
            if (!_loginService.IsLoggedIn) // Check if the user is logged in
            {
                Console.WriteLine("You must be logged in to send a message.");
                return;
            }

            Console.WriteLine("Enter the massage to be sent: ");
            string data = Console.ReadLine();
            _socketClient.Send(data);
        }

        protected void AddData()
        {
            if (!_loginService.IsLoggedIn) // Check if the user is logged in
            {
                Console.WriteLine("You must be logged in to add data.");
                return;
            }

            int commandType = 01; //This is the sign of add command
            Console.WriteLine("Fill the form with the data values.");
            Console.WriteLine("Those are the data types: item_name: , quantity:, price_per_unit:");

            Console.WriteLine("Add the item_name");
            string item_name = Console.ReadLine();

            Console.WriteLine($"Add the quantity of {item_name}");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Add the price of the each unit");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            decimal total = price * quantity;
            Console.WriteLine($"item_name: {item_name}| quantity: {quantity}| price: £{price}| total: £{total}");

            Console.WriteLine();

            Console.WriteLine("Type 1 if you would like to proceed with the current data or type any other number if you would like to restart");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice != 1)
            {
                return;
            }
            else
            {
               _socketClient.Send(GetFinalData(commandType, item_name, quantity, price));
                           
            }   
        }

        private string GetFinalData(int commandType, string item_name, int quantity, decimal pricePerUnit)
        {
            string finalData = $"{commandType.ToString()}, {item_name}, {quantity.ToString()}, {pricePerUnit.ToString()}";
            return finalData;
        }
    }
}
