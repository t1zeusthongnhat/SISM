using Newtonsoft.Json;
using StudentManager.Models;

namespace StudentManager.Services.Imp
{
    public class UserService : IUserService
    {
        private readonly string _filePath;
        public UserService()
        {
            // Đường dẫn tới file user.json
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "user.json");
        }

        public User GetUser(string username)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(_filePath));
            return users.FirstOrDefault(u => u.Username == username);
        }

        public void CreateUser(User user)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(_filePath)) ?? new List<User>();

            // Tự động tăng Id
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(users, Formatting.Indented));
        }

        public List<User> GetUsers()
        {
            var jsonData = File.ReadAllText(_filePath);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonData);
            return users;
        }
    }
}
