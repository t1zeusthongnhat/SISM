using Microsoft.AspNetCore.Mvc;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Services;

namespace TestApp
{
    public class Login
    {
        [Fact]
        public void Index_ReturnsViewResult_ForGetRequest()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var controller = new LoginController(mockUserService.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}