using Server.DataBase.Entity;
using Server.DataBase.Repository;
using Server.Utils;

namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Dictionary<string, int> _session;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUser(int id)
        {
            return _unitOfWork.Users.Get(id);
        }
        
        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll();
        }

        public void CreateUser(User user)
        {
            var existingUsers = _unitOfWork.Users.Get(user.UserID);
            if (existingUsers != null) return;

            user.Password = HashingUtility.HashString(user.Password); 
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }

        public void UpdateUser(User user)
        {
            var existingUsers = _unitOfWork.Users.Get(user.UserID);
            if (existingUsers != null)
            {
                //Update props
                _unitOfWork.Complete();
            }
        }
        public void DeleteUser(int id)
        { 
            var user = _unitOfWork.Users.Get(id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(user);
                _unitOfWork.Complete();
            }
        }

        public User GetUserByUsername(string username)
        {
            return _unitOfWork.Users.Find(u => u.Username == username).SingleOrDefault();
        }

        public string Login(string data)

        {
            //No needto check if the string is empty since if it empty it will not pass the check in the menu option.
            string[] parts = data.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string username = parts[0];
            string password = parts[1];

            //get whole data in a chunk split it in username and password vars 
            var user = GetUserByUsername(username);
            if (user == null) return null;

            //Password should be sent as a hash from the client
            if (user.Password == password)
            {
                //Generate a session token
                string sessionToken = Guid.NewGuid().ToString();
                _session[sessionToken] = user.UserID;
                return sessionToken; // Return the session token to the client

            }
            return null; //Unable to login
        }

        public bool IsAuthenticated(string token)
        {
            return _session.ContainsKey(token);
        }

        public void Logout(string token)
        {
            if (_session.ContainsKey(token))
            {
                _session.Remove(token);
            }
        }
    }
}
