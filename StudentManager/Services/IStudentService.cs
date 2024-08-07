using StudentManager.Models;

namespace StudentManager.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        int GetStudentCount();
        public void AddStudent(Student student);
        Student GetStudentById(int id);
        void UpdateStudent(Student student);
    }
}
