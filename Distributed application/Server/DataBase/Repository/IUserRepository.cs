using Server.DataBase.Entity;
using System.Linq.Expressions;

namespace Server.DataBase.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> Find(Expression<Func<User, bool>> predicate);
    }
}
