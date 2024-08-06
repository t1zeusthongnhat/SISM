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
            // Phân biệt hành động dựa trên sự có mặt của tham số email
            if (email == null)
            {
                // Xử lý đăng nhập
                var user = _userService.GetUser(username);

                if (user != null && user.Password == password)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Thông tin đăng nhập không chính xác.";
                return View();
            }
            else
            {
                // Xử lý đăng ký
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Tất cả các trường đều là bắt buộc.";
                    return View();
                }

                var existingUser = _userService.GetUser(username);
                if (existingUser != null)
                {
                    ViewBag.Error = "Tên người dùng đã tồn tại.";
                    return View();
                }

                var newUser = new User
                {
                    Username = username,
                    Email = email,
                    Password = password // Nên mã hóa mật khẩu trước khi lưu trữ
                };

                _userService.CreateUser(newUser);
                ViewBag.Message = "Đăng ký thành công!";
                return View();
            }
        }
    }
}