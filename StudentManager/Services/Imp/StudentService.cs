using Newtonsoft.Json;
using StudentManager.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Converters;

namespace StudentManager.Services.Imp
{
    public class StudentService : IStudentService
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _jsonSettings;

        public StudentService()
        {
            // Đường dẫn tới file student.json
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "student.json");

            // Cài đặt định dạng ngày tháng
            _jsonSettings = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy"
            };
        }

        // Lấy sinh viên theo ID
        public Student GetStudentById(int id)
        {
            var students = GetAllStudents();
            return students.FirstOrDefault(s => s.Id == id);
        }

        // Cập nhật sinh viên
        public void UpdateStudent(Student student)
        {
            var students = GetAllStudents();
            var index = students.FindIndex(s => s.Id == student.Id);
            if (index != -1)
            {
                students[index] = student;
                var jsonString = JsonConvert.SerializeObject(students, _jsonSettings);
                File.WriteAllText(_filePath, jsonString);
            }
        }

        // Thêm sinh viên, ID tự động tăng
        public void AddStudent(Student student)
        {
            var students = GetAllStudents();
            student.Id = students.Any() ? students.Max(s => s.Id) + 1 : 1;
            students.Add(student);
            var jsonString = JsonConvert.SerializeObject(students, _jsonSettings);
            File.WriteAllText(_filePath, jsonString);
        }

        // Lấy tất cả sinh viên
        public List<Student> GetAllStudents()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Student>>(jsonData, _jsonSettings);
        }

        // Đếm số lượng sinh viên
        public int GetStudentCount()
        {
            var students = GetAllStudents();
            return students.Count;
        }

        public void DeleteStudent(int id)
        {

            var students = GetAllStudents();
            var studentToRemove = students.FirstOrDefault(s => s.Id == id);
            if (studentToRemove != null)
            {
                students.Remove(studentToRemove);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(students));
            }
        }

         public List<Student> SearchStudents(string keyword)
        {
        var students = GetAllStudents();
        return students.Where(s => 
            s.StudentName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            s.Gender.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            s.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            s.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            s.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
        public List<Student> GetStudentsPaged(int pageNumber, int pageSize)
        {
            return GetAllStudents()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}