using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;
using StudentManager.Services.Imp;

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

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);
                TempData["success"] = "User added successfully!";
                return RedirectToAction("Index", "User");
            }

            TempData["error"] = "There was an error adding the User.";
            return View(user);
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var users = _userService.GetUserById(id);
            if(users== null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(user);
                TempData["success"] = "Update success!";
                return RedirectToAction("Index", "User");
            }

            TempData["error"] = "Error.";
            return View(user);
        }
    }
}
