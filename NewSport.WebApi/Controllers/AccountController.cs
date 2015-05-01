using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;
using NewSport.WebApi.Models;
using Ninject.Infrastructure.Language;

namespace NewSport.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User
        public ActionResult Index()
        {
            var users = _userRepository.Users.ToEnumerable();
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.LogIn(model.Username, model.Password))
                {
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("","Nieprawidłowa nazwa użytkownika lub hasło");
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Username,Email,Password,Roles")]User user )
        {
            if (ModelState.IsValid)
            {
                user.Roles = "USER";
                _userRepository.Save(user);
                return RedirectToAction("Index", "Post");
            }
            return View(user);
        }
        [Authorize]
        public ActionResult LogOff()
        {
            _userRepository.LogOff();
            return RedirectToAction("Index","Post");
        }
    }
}