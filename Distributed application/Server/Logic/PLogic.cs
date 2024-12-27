using Server.Connection;
using Server.DataBase.Entity;
using Server.Services;
using System.Text.RegularExpressions;

namespace Server.Logic
{
    public abstract class PLogic
    {

        //TODO Update the functions that interact withb the db

        protected readonly IUserService _userService;
        protected readonly IItemService _itemService;

        public PLogic(IUserService userService, IItemService itemService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }
    }
}
