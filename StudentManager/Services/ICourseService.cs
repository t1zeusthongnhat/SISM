using StudentManager.Models;

namespace StudentManager.Services
{
    public interface ICourseService
    {
        List<Course> gettDataCourse();
        int GetCourseCount();
        public void AddCourse(Course course);

        Course GetCourseById(int id);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        int CountCourse();
        List<Course> SearchCourses(string keyword);
        List<Course> GetCoursesPaged(int pageNumber, int pageSize);

      
    }
}
