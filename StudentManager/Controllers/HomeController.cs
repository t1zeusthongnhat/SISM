using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using System.Diagnostics;

namespace StudentManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string SessionKeyName = "UserName";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString(SessionKeyName);
            if (string.IsNullOrEmpty(name))
            {
                name = "Guest"; // Giá trị mặc định nếu không có dữ liệu trong session
            }
            ViewBag.Username = name;

            _logger.LogInformation("Session Name: {Name}", name);

            return View();
        }

       
    }
}
