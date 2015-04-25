using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;
using NewSport.WebApi.Controllers;
using NewSport.WebApi.Models;

namespace NewSport.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _userController;
        private Mock<IUserRepository> _mock;
        private List<User> _users;

        [TestInitialize]
        public void Execute()
        {
            _users = new List<User>()
            {
                new User(){Username = "username",Password = "password"}
            };
            _mock = new Mock<IUserRepository>();
            _mock.Setup(u => u.Users).Returns(_users.AsQueryable());
            _mock.Setup(u => u.LogIn("username", "password")).Returns(true);
            _userController = new UserController(_mock.Object);
        }

        [TestMethod]
        public void LoginTestWithValidData()
        {
            LoginViewModel viewModel = new LoginViewModel()
            {
                Username = "username",Password = "password"
            };
            var result = _userController.SignUp(viewModel);
            Assert.IsInstanceOfType(result,typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void LoginTestWithInValidData()
        {
            LoginViewModel viewModel = new LoginViewModel()
            {
                Username = "uaa",
                Password = "password"
            };
            var result = _userController.SignUp(viewModel);
            Assert.IsNotInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
