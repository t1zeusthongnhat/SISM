using Newtonsoft.Json;
using StudentManager.Models;
using StudentManager.Service.IService;

namespace StudentManager.Service
{
    public class UserService: IUserService
    {
        private readonly string _filePath;

        public UserService(string filePath)
        {
            _filePath = filePath;
        }

        public List<User> GetAllUsers()
        {
            var jsonData = System.IO.File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
