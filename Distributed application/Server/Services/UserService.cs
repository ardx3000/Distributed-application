using Server.DataBase.Entity;
using Server.DataBase.Repository;

namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork _unitOfWork)
        {
            _unitOfWork = _unitOfWork;
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
            var existingUsers = _unitOfWork.Users.Get(user.Id);
            if (existingUsers != null) return;
            
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }

        public void UpdateUser(User user)
        {
            var existingUsers = _unitOfWork.Users.Get(user.Id);
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
    }
}
