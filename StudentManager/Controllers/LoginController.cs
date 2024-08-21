using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using StudentManager.Services;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password, string email = null)
        {
            if (email == null)
            {

                
                var user = _userService.GetUser(username);

                if (user != null && user.Password == password)
                {
                    HttpContext.Session.SetString("SessionRole", user.Role);
                    HttpContext.Session.SetString("SessionStatus", user.Status);
                    var role = HttpContext.Session.GetString("SessionRole");
                    var status = HttpContext.Session.GetString("SessionStatus");

                    if (user.Status == "Active")
                    {
                        TempData["success"] = "Login successful!";

                        if (role == "Admin")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (role == "Student")
                        {
                            return RedirectToAction("onlyViewStudent", "Student");
                        }
                       

                        TempData["success"] = "Login successful!";

                        // Redirect based on role
                       
                    }
                    else if (status == "Deactive")
                    {
                        TempData["error"] = "Your account has been locked!";
                        return View();
                    }
                }


                TempData["error"] = "Incorrect login information.";
                return View();
            }
            else
            {
                // Handle registration
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    TempData["error"] = "All fields are required.";
                    return View();
                }

                var existingUser = _userService.GetUser(username);
                if (existingUser != null)
                {
                    TempData["error"] = "Username already exists.";
                    return View();
                }

                var newUser = new User
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    Status = "Active",   // Automatically set status to "Active"
                    Role = "Student"
                };

                _userService.CreateUser(newUser);
                TempData["success"] = "Registration successful!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["success"] = "Logged out successfully!";
            return RedirectToAction("Index");
        }
    }
}