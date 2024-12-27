using Server.Services;

namespace Server.Logic
{
    public class LoginManagementLogic : PLogic
    {
        public LoginManagementLogic(IUserService userService, IItemService itemService) : base(userService, itemService)
        {
        }

        public void Login(string data)
        {
            Console.WriteLine("data in login"+data);
            _userService.Login(data);

        }
    }
}
