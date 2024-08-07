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

        public void AddCourse(Course course)
        {
            var courses = gettDataCourse();
            if (courses.Any())
            {
               course.Id = courses.Max(s => s.Id) + 1;
            }
            else
            {
                course.Id = 1;
            }
            courses.Add(course);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(courses));
        }

        public Course GetCourseById(int id)
        {
            var courses = gettDataCourse();
            return courses.FirstOrDefault(s => s.Id == id);
             
        }


        public void UpdateCourse(Course course)
        {
            var courses = gettDataCourse();
            var index = courses.FindIndex(s => s.Id == course.Id);
            if (index != -1)
            {
                courses[index] = course; // Cập nhật thông tin khóa học tại vị trí index
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(courses));
            }
        }
    }
}
