using System.Collections.Generic;
using System.Linq;
using Server.DataBase.UserCalsses;

namespace Server.DataBase.Repository
{
    public class UserRepository : BaseRepository<User> 
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
