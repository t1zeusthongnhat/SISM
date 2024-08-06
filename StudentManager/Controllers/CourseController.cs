using Microsoft.AspNetCore.Mvc;
using StudentManager.Services;

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
    }
}
