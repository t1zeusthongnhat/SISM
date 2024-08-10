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
        void DeleteStudent(int id);
        List<Student> SearchStudents(string keyword);
        List<Student> GetStudentsPaged(int pageNumber, int pageSize);
    }
}
