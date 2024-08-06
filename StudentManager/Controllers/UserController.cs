using Microsoft.AspNetCore.Mvc;
using StudentManager.Services;

namespace StudentManager.Controllers
{
    public class UserController : Controller
    {


        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        public IActionResult createUser()
        {
            return View();
        }

        public IActionResult editUser()
        {
            return View();
        }
    }
}
