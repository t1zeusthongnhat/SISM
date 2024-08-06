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

        [Fact]
        public void Index_ReturnsRedirectResult_ForValidLogin()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(s => s.GetUser("validuser"))
                .Returns(new User { Username = "validuser", Password = "validpassword" });

            var controller = new LoginController(mockUserService.Object);

            // Act
            var result = controller.Index("validuser", "validpassword");

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public void Index_ReturnsViewResult_ForInvalidLogin()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(s => s.GetUser("invaliduser"))
                .Returns((User)null);

            var controller = new LoginController(mockUserService.Object);

            // Act
            var result = controller.Index("invaliduser", "wrongpassword");

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }