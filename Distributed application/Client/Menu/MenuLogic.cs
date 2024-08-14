
namespace Client.Menu
{
    public class MenuLogic 
    {
        protected void AddData()
        {
            Console.WriteLine("Fill the form with the data values.");
            Console.WriteLine("Those are the data types: item_name: , quantity:, price_per_unit:");

            Console.WriteLine("Add the item_name");
            string item_name = Console.ReadLine();

            Console.WriteLine($"Add the quantity of {item_name}");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Add the price of the each unit");
            double price = Convert.ToDouble(Console.ReadLine());

            double total = price * quantity;
            Console.WriteLine($"item_name: {item_name}| quantity: {quantity}| price: £{price}| total: £{total}");

            Console.WriteLine();

            Console.WriteLine("Would you like to procceed with this data or you woul like to start again");

            //int choice = Convert.ToInt32(Console.ReadLine());

            //TODO create a condition that will confirm the data and send it to the server or the user can repeat the process.

        }
    }
}
