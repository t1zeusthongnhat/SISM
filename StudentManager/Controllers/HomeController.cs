using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace StudentManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public HomeController(IStudentService studentService, ICourseService courseService, IUserService userService)
        {
            _studentService = studentService;
            _courseService = courseService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var studentCount = _studentService.GetStudentCount();
            var courseCount = _courseService.GetCourseCount();
            var userscount = _userService.GetUsersCount();

            ViewBag.StudentCount = studentCount;
            ViewBag.CourseCount = courseCount;
            ViewBag.UserCount = userscount;

            return View();
        }


       


    }
}
