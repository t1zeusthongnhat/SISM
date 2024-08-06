using Newtonsoft.Json;
using StudentManager.Models;

namespace StudentManager.Services.Imp
{
    public class StudentService : IStudentService
    {
        private readonly string _filePath;
        public StudentService()
        {
            // Đường dẫn tới file user.json
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "student.json");
        }

        public List<Student> GetAllStudents()
        {
            var jsonData = File.ReadAllText(_filePath);
            var students = JsonConvert.DeserializeObject<List<Student>>(jsonData);
            return students;
        }
    }
}
