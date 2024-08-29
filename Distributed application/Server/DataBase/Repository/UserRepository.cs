using Server.DataBase.Entity;
using System.Linq.Expressions;

namespace Server.DataBase.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {

        }
        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }
    }
}
