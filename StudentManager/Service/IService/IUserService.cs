using StudentManager.Models;

namespace StudentManager.Service.IService
{
    public interface IUserService
    {
        public User GetUserByUsernameAndPassword(string username, string password);
        List<User> GetAllUsers();
    }
}
