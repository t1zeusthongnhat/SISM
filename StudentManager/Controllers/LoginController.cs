using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "user.json");
        private const string SessionKeyName = "UserName";

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private List<User> GetAllUsers()
        {
            var jsonData = System.IO.File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {

                _httpContextAccessor.HttpContext.Session.SetString(SessionKeyName, user.Username); // Lưu email vào Session
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
