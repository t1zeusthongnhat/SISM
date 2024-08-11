using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using StudentManager.Models;
using StudentManager.Services;

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
                // Xử lý đăng nhập
                var user = _userService.GetUser(username);
                if (user != null && user.Password == password)
                {
                    if (user.Status == "Active")
                    {
                        

                        TempData["success"] = "Logged in successfully!";
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Status == "Deactive")
                    {
                        TempData["error"] = "Your account has been blocked!!!";
                        return View();
                    }
                }

                TempData["error"] = "Login information is incorrect.";
                return View();
            }
            else
            {
                // Xử lý đăng ký
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
                    Password = password, // Mã hóa mật khẩu trước khi lưu trữ
                    Status = "Active"   // Tự động đặt trạng thái là "Active"
                };

                _userService.CreateUser(newUser);
                TempData["success"] = "Sign Up Success!";
                return RedirectToAction("Index");
            }
        }
    }
}