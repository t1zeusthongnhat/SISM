using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using StudentManager.Models;
using StudentManager.Service.IService;

namespace StudentManager.Controllers
{
    public class LoginController : Controller
    {

       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public LoginController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var user = _userService.GetUserByUsernameAndPassword(username, password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
