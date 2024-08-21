using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;
using StudentManager.Services.Imp;
using System.Reflection;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace StudentManager.Controllers
{
    [ServiceFilter(typeof(RoleAuthorize))]
    public class CourseController : Controller
    {
        
        private ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public IActionResult onlyViewCourse(int pageNumber = 1, int pageSize = 4)
        {
            var courses = _courseService.GetCoursesPaged(pageNumber, pageSize);
            var totalCourse = _courseService.GetCourseCount();
            var totalPages = (int)Math.Ceiling((double)totalCourse / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(courses);
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {
            
            var courses = _courseService.GetCoursesPaged(pageNumber, pageSize);
            var totalCourse = _courseService.GetCourseCount();
            var totalPages = (int)Math.Ceiling((double)totalCourse / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

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


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _courseService.DeleteCourse(id);
            TempData["success"] = "Course deleted successfully!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
           var courses = _courseService.GetCourseById(id);
            if(courses == null )
            {
                return View("NotFound");
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

        public IActionResult Search(string keyword, int pageNumber = 1, int pageSize = 4)
        {
            var coursess = _courseService.SearchCourses(keyword);
            var totalCourse = coursess.Count;
            var totalPages = (int)Math.Ceiling((double)totalCourse / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchKeyword = keyword; // Lưu trữ từ khóa tìm kiếm trong ViewBag để sử dụng trong view

            // Phân trang cho danh sách kết quả tìm kiếm
            var oagedTotal = coursess
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("Index", oagedTotal);
        }

        public IActionResult ViewSearch(string keyword, int pageNumber = 1, int pageSize = 4)
        {
            var coursess = _courseService.SearchCourses(keyword);
            var totalCourse = coursess.Count;
            var totalPages = (int)Math.Ceiling((double)totalCourse / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchKeyword = keyword; // Lưu trữ từ khóa tìm kiếm trong ViewBag để sử dụng trong view

            // Phân trang cho danh sách kết quả tìm kiếm
            var oagedTotal = coursess
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("onlyViewCourse", oagedTotal);
        }


    }
}
