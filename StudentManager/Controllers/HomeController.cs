using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;
using System.Diagnostics;

namespace StudentManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public HomeController(IStudentService studentService, ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }




        public IActionResult Index()
        {
            var studentCount = _studentService.GetStudentCount();
            var courseCount = _courseService.GetCourseCount();

            ViewBag.StudentCount = studentCount;
            ViewBag.CourseCount = courseCount;

            return View();
        }

       
    }
}
