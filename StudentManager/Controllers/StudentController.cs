using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Services;

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

        TempData["error"] = "There was an error adding the student.";
        return View(student);
    }

    [HttpGet]
    public IActionResult EditStudent(int id) // Thêm tham số id
    {
        var student = _studentService.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
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
}