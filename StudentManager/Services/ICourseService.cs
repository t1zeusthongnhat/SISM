using StudentManager.Models;

namespace StudentManager.Services
{
    public interface ICourseService
    {
        List<Course> gettDataCourse();
        int GetCourseCount();
    }
}
