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
        public string Login(string username, string password);
        public User GetUserByUsername(string username);
        public bool IsAuthenticated(string token);
        public void Logout(string token);
    }
}
