using Newtonsoft.Json;
using StudentManager.Models;

namespace StudentManager.Services.Imp
{
    public class CourseService : ICourseService
    {
        private readonly string _filePath;
        public CourseService()
        {
            // Đường dẫn tới file user.json
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "course.json");
        }
        public List<Course> gettDataCourse()
        {
            var jsonData = File.ReadAllText(_filePath);
            var courses = JsonConvert.DeserializeObject<List<Course>>(jsonData);
            return courses;
        }
        public int GetCourseCount()
        {
            var jsonData = File.ReadAllText(_filePath);
            var course = JsonConvert.DeserializeObject<List<Course>>(jsonData);
            return course.Count;
        }
    }
}
