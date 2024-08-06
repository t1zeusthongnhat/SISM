using StudentManager.Models;

namespace StudentManager.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        int GetStudentCount();
    }
}
