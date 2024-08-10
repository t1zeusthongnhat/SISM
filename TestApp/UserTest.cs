using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Controllers;
using StudentManager.Models;
using StudentManager.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TestApp
{
    public class UserTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;

        public UserTest()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);

            var tempData = new Mock<ITempDataDictionary>();
            _controller.TempData = tempData.Object;
        }

        [Fact]
        public void CreateUser_ReturnsRedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var user = new User { Username = "TestUser", Email = "test@example.com", Password = "password", Role = "Admin", Status = "Active" };

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("User", redirectToActionResult.ControllerName);

            // Verify that the service's AddUser method was called once
            _mockUserService.Verify(s => s.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void CreateUser_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var user = new User { Username = "", Email = "test@example.com", Password = "password", Role = "Admin", Status = "Active" };
            _controller.ModelState.AddModelError("Username", "Required");

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(user, viewResult.Model);

            // Verify that the service's AddUser method was never called
            _mockUserService.Verify(s => s.AddUser(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAListOfUsers()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetUsers()).Returns(GetTestUsers());

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }


        [Fact]
        public void EditUser_ReturnsNotFoundResult_WhenUserNotFound()
        {
            // Arrange
            int userId = 1;
            _mockUserService.Setup(service => service.GetUserById(userId)).Returns((User)null);

            // Act
            var result = _controller.EditUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditUser_ReturnsViewResult_WithUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "TestUser", Email = "test@example.com", Password = "password" };
            _mockUserService.Setup(service => service.GetUserById(user.Id)).Returns(user);

            // Act
            var result = _controller.EditUser(user.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(user, viewResult.Model);
        }

        private List<User> GetTestUsers()
        {
            return new List<User>
        {
            new User { Id = 1, Username = "User1", Email = "user1@example.com", Password = "password" },
            new User { Id = 2, Username = "User2", Email = "user2@example.com", Password = "password" }
        };
        }
    }
}
