using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;
using StudentManager.Services.Imp;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace StudentManager.Controllers
{

    [ServiceFilter(typeof(RoleAuthorize))] 
    public class StudentController : Controller
    {

        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult onlyViewStudent(int pageNumber = 1, int pageSize = 4)
        {
            var students = _studentService.GetStudentsPaged(pageNumber, pageSize);
            var totalUsers = _studentService.GetStudentCount();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(students);
        }
        public IActionResult ViewSearch(string keyword, int pageNumber = 1, int pageSize = 4)
        {
            var students = _studentService.SearchStudents(keyword);
            var totalStudent = students.Count;
            var totalPages = (int)Math.Ceiling((double)totalStudent / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchKeyword = keyword; // Lưu trữ từ khóa tìm kiếm trong ViewBag để sử dụng trong view

            // Phân trang cho danh sách kết quả tìm kiếm
            var pagedTotal = students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("onlyViewStudent", pagedTotal);
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {
            var students = _studentService.GetStudentsPaged(pageNumber, pageSize);
            var totalUsers = _studentService.GetStudentCount();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(students);
        }



        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.AddStudent(student);
                TempData["success"] = "Student added successfully!";
                return RedirectToAction("Index", "Student");
            }

            TempData["error"] = "There was an error adding the Student.";
            return View(student);
        }
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return View("NotFound");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(student);
                TempData["success"] = "Update success!";
                return RedirectToAction("Index", "Student");
            }

            TempData["error"] = "Error.";
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _studentService.DeleteStudent(id);
            TempData["success"] = "Student deleted successfully!";
            return RedirectToAction("Index");
        }


        public IActionResult Search(string keyword, int pageNumber = 1, int pageSize = 4)
        {
            var students = _studentService.SearchStudents(keyword);
            var totalStudent = students.Count;
            var totalPages = (int)Math.Ceiling((double)totalStudent / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchKeyword = keyword; // Lưu trữ từ khóa tìm kiếm trong ViewBag để sử dụng trong view

            // Phân trang cho danh sách kết quả tìm kiếm
            var pagedTotal = students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("Index", pagedTotal);
        }
    }
}