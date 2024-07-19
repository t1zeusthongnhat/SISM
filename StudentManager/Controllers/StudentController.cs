using Microsoft.AspNetCore.Mvc;

namespace StudentManager.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
