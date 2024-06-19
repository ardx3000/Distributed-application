using Server.DataBase.Entity;

namespace Server.Services
{
    public interface IUserService
    {
        User GetUser(int Id);
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int Id);
    }
}
