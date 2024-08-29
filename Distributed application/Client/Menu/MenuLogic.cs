
using Client.Connection;

namespace Client.Menu
{
    public class MenuLogic 
    {
        protected SocketClient _socketClient; // Protected field to store the socket client

        public MenuLogic(SocketClient socketClient)
        {
            _socketClient = socketClient; // Initialize the socket client
        }

        protected void TestSend()
        {
            Console.WriteLine("Enter the massage to be sent: ");
            string data = Console.ReadLine();
            _socketClient.Send(data);
        }

        protected void AddData()
        {
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
