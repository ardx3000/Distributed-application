using Server.Connection;
using Server.DataBase.Entity;
using Server.Services;
namespace Server.Menu
{
    public class MenuLogic
    {

        //TODO Update the functions that interact withb the db

        private readonly IUserService _userService;

        public MenuLogic(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
            string password = Console.ReadLine();
            string setPassword;

            //TODO create a method to confirm password,
            //TODO create a method that hashes the password.
          /*  bool confirmPasswordind = false;
            int tries = 0;
            while (confirmPasswordind == false)
            {
                Console.WriteLine("Confirm password");
                string confirmPassword = Console.ReadLine();
                if (confirmPassword == password)
                {
                    setPassword = password;
                    confirmPasswordind = true;
                }
                else
                {
                    tries++;
                    Console.WriteLine("Incorect password please try again");
                    if (tries == 5) Console.WriteLine("To many attempts! "); return;
                }
            }*/

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
                Console.WriteLine($"User ID: {user.userID}| Username: {user.Username}| Role: {user.Role}");

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
                Console.WriteLine($"User ID: {user.userID}| Username: {user.Username}| Role: {user.Role}");
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
    }
}
