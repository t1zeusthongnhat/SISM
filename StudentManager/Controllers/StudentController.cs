using Microsoft.AspNetCore.Mvc;
using StudentManager.Services;

namespace StudentManager.Controllers
{
    public class StudentController : Controller
    {

        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var students = _studentService.GetAllStudents();
            return View(students);
        }
    }
}
