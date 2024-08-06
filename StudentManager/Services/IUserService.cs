using StudentManager.Models;

namespace StudentManager.Services
{
    public interface IUserService
    {
        User GetUser(string username);
        void CreateUser(User user);
        List<User> GetUsers(); 


    }
}
