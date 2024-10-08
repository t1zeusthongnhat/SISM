﻿using Newtonsoft.Json;
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
       

        public void AddUser(User user)
        {
            var users = GetUsers();
            if (users.Any())
            {
                user.Id = users.Max(s => s.Id) + 1;
            }
            else
            {
                user.Id = 1;
            }
            users.Add(user);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(users));
        }

        private List<User> GetUserss()
        {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public User GetUserById(int id)
        {
            var users = GetUserss();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            var users = GetUserss();
            var index = users.FindIndex(s => s.Id == user.Id);
            if (index != -1)
            {
                users[index] = user;
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(users));
            }
        }

        public void DeleteUsers(int id)
        {
            var users = GetUserss();
            var userRemove = users.FirstOrDefault(s => s.Id == id);
            if (userRemove != null)
            {
                users.Remove(userRemove);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(users));
            }
        }

        public List<User> SearchUsers(string keyword)
        {
            var users = GetUserss();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<User>(); // Trả về danh sách rỗng nếu keyword trống
            }

            return users.Where(s =>
                s.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Status.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<User> GetUsersPaged(int pageNumber, int pageSize)
        {
            return GetUserss()
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();
        }

        public int GetUsersCount()
        {
            var users = GetUserss();
            return users.Count;
        }
      

    }
}
