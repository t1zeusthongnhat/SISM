using StudentManager.Models;

namespace StudentManager.Services
{
    public interface IUserService
    {
        User GetUser(string username);
        void CreateUser(User user);
        List<User> GetUsers();
        public void AddUser(User user);

        User GetUserById(int id);
        void UpdateUser(User user);


    }
}
