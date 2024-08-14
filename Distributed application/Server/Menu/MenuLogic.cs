﻿using Server.Connection;
using Server.DataBase.Entity;
using Server.Services;
using Server.Utils;
namespace Server.Menu
{
    public class MenuLogic
    {

        //TODO Update the functions that interact withb the db

        private readonly IUserService _userService;
        private readonly IItemService _itemService;

        public MenuLogic(IUserService userService, IItemService itemService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }

        protected void DisplayConnectedUsers()
        {
            SocketServer.DisplayConnectedClients();
        }

        protected void AddUser()
        {
            Console.WriteLine("Enter user name: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password: ");
            string password = HashingUtility.HashString(Console.ReadLine());

            //TODO create a method to confirm password,

            Console.WriteLine("Enter user's role using a number from 0 to 3: ");
            int role = Convert.ToInt32(Console.ReadLine());

            var newUser = new User { Username = username, Password = password, Role = role };
            _userService.CreateUser(newUser);

            Console.WriteLine($"User {username} has been created succesfully! ");
            return;
        }
        protected void UpdateUser()
        {
            return;
        }
        protected void GetUser()
        {
            Console.WriteLine("Ente user ID: ");
            int userID = Convert.ToInt32(Console.ReadLine());

            var user = _userService.GetUser(userID);
            if (user != null)
            {
                Console.WriteLine($"User ID: {user.UserID}| Username: {user.Username}| Role: {user.Role}");

            }
            else
            {
                Console.WriteLine("User not found");
            }

            return;
        }
        protected void GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.UserID}| Username: {user.Username}| Role: {user.Role}");
            }

            return;
        }
        protected void DeleteUser()
        {
            Console.WriteLine("Enter user ID to be deleted: ");
            int userID = Convert.ToInt32(Console.ReadLine());

            _userService.DeleteUser(userID);
            Console.WriteLine("USer deleted succesfullt! ");

            return;
        }

        protected void AddItem()
        {
            Console.WriteLine("Enter Item name");
            string itemName = Console.ReadLine();

            Console.WriteLine("Enter Item quantity");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter price per unit");
            decimal pricePerUnit = Convert.ToDecimal(Console.ReadLine());

            var newItem = new Items { Name = itemName, Quantity = quantity, PricePerUnit = pricePerUnit, UserID = 2 /* User id is a hard coded placehorder
                                                                                                                     This will be updated to take the actual logged in user*/ };
            _itemService.AddOrUpdateItem(newItem);

            Console.WriteLine($"Item {itemName} has been added succesfully! ");
            return;
        }
    }
}
