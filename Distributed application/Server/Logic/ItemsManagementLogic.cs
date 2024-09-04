using Server.DataBase.Entity;
using Server.Services;
using System.Text.RegularExpressions;

namespace Server.Logic
{
    public class ItemsManagementLogic : PLogic
    {
        public ItemsManagementLogic(IUserService userService, IItemService itemService) : base(userService, itemService)
        {
        }

        public void AddData(string itemName, int quantity, decimal pricePerUnit)
        {
            var newItem = new Items
            {
                Name = itemName,
                Quantity = quantity,
                PricePerUnit = pricePerUnit,
                UserID = 1 //Hard cocerd for the moment 
            };

            _itemService.AddOrUpdateItem(newItem);

            Console.WriteLine($"Item {itemName} has been added successfully!");
        }

        public void AddItem(string data)
        {
            string pattern = @"Item_name:\s*(?<item_name>[^,]+),\s*Quantity:\s*(?<quantity>\d+),\s*Price:\s*(?<price>\d+)";

            Match match = Regex.Match(data, pattern);
            if (match.Success)
            {
                string Item_name = match.Groups["item_name"].Value;
                string Quantity = match.Groups["quantity"].Value;
                string Price = match.Groups["price"].Value;

                int quantity = Convert.ToInt32(Quantity);
                decimal price = Convert.ToDecimal(Price);

                AddData(Item_name, quantity, price);

                Console.WriteLine(Item_name + Quantity + Price);
            };
        }
    }
}
