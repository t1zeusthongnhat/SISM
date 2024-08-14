using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class LoginControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly LoginController _controller;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ITempDataDictionary> _mockTempData;

        public LoginControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new LoginController(_mockUserService.Object);
            _mockHttpContext = new Mock<HttpContext>();
            _mockTempData = new Mock<ITempDataDictionary>();
            _controller.TempData = _mockTempData.Object;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _mockHttpContext.Object
            };
        }

        [Fact]
        public void Index_ReturnsViewResult_ForLoginGet()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ReturnsRedirectToActionResult_ForValidLogin()
        {
            // Arrange
            var user = new User { Username = "TestUser", Password = "password", Role = "Admin", Status = "Active" };
            _mockUserService.Setup(s => s.GetUser(user.Username)).Returns(user);

            // Act
            var result = _controller.Index(user.Username, user.Password);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void Index_ReturnsViewResult_ForDeactiveUser()
        {
            // Arrange
            var user = new User { Username = "TestUser", Password = "password", Status = "Deactive" };
            _mockUserService.Setup(s => s.GetUser(user.Username)).Returns(user);

            // Act
            var result = _controller.Index(user.Username, user.Password);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _mockTempData.VerifySet(tempData => tempData["error"] = "Your account has been locked!");
        }

        [Fact]
        public void Index_ReturnsViewResult_ForInvalidLogin()
        {
            // Arrange
            var username = "InvalidUser";
            _mockUserService.Setup(s => s.GetUser(username)).Returns((User)null);

            // Act
            var result = _controller.Index(username, "wrongPassword");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _mockTempData.VerifySet(tempData => tempData["error"] = "Incorrect login information.");
        }

        [Fact]
        public void Index_ReturnsRedirectToActionResult_ForSuccessfulRegistration()
        {
            // Arrange
            var username = "NewUser";
            var email = "newuser@example.com";
            var password = "password";
            _mockUserService.Setup(s => s.GetUser(username)).Returns((User)null);

            // Act
            var result = _controller.Index(username, password, email);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockTempData.VerifySet(tempData => tempData["success"] = "Registration successful!");
            _mockUserService.Verify(s => s.CreateUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Logout_ReturnsRedirectToActionResult()
        {
            // Act
            var result = _controller.Logout();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockTempData.VerifySet(tempData => tempData["success"] = "Logged out successfully!");
        }
    }
}
