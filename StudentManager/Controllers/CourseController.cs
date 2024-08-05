using Microsoft.AspNetCore.Mvc;

namespace StudentManager.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
