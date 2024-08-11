﻿using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {


            var users = _userService.GetUsersPaged(pageNumber, pageSize);
            var totalusers = _userService.GetUsersCount();
            var totalPages = (int)Math.Ceiling((double)totalusers / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUsers(id);
            TempData["success"] = "Users deleted successfully!";
            return RedirectToAction("Index");
        }


        public IActionResult Search(string keyword, int pageNumber = 1, int pageSize = 4)
        {
            var users = _userService.SearchUsers(keyword);
            var totalusers = users.Count;
            var totalPages = (int)Math.Ceiling((double)totalusers / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            // Phân trang cho danh sách kết quả tìm kiếm
            var pagedtotalusers = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("Index", pagedtotalusers);
        }
    }
}
