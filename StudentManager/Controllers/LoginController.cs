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
                    if (user.Status == "Active")
                    {
                        // Create authentication cookie
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true // Remains across sessions
                        };

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        TempData["success"] = "Login successful!";

                        // Redirect based on role
                        if (user.Role == "Admin")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (user.Role == "Student" || user.Role == "Teacher")
                        {
                            return RedirectToAction("onlyViewStudent", "Student");
                        }
                    }
                    else if (user.Status == "Deactive")
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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["success"] = "Logged out successfully!";
            return RedirectToAction("Index");
        }
    }
}