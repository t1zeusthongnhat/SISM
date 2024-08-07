using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;
using StudentManager.Services.Imp;
using System.Reflection;

namespace StudentManager.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            var courses = _courseService.gettDataCourse();
            return View(courses);
        }
        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.AddCourse(course);
                TempData["success"] = "Course added successfully!";
                return RedirectToAction("Index", "Course");
            }

            TempData["error"] = "There was an error adding the Course.";
            return View(course);
        } 
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
           var courses = _courseService.GetCourseById(id);
            if(courses == null )
            {
                return NotFound();
            }
            return View(courses);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {

            if (ModelState.IsValid)
            {
                _courseService.UpdateCourse(course);
                TempData["success"] = "Update success!";
                return RedirectToAction("Index", "Course");
            }

            TempData["error"] = "Error.";
            return View(course);
        }
    }
}
