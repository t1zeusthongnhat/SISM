using StudentManager.Services;

namespace TestApp
{
    internal class Mock<T>
    {
        public Mock()
        {
        }

        public IUserService Object { get; internal set; }
    }
}