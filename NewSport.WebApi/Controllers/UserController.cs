﻿using System;
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
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
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
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.LogIn(model.Username, model.Password))
                {
                    Session["user"] = model.Username;
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
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "Id,Username,Email,Password")]User user )
        {
            if (ModelState.IsValid)
            {
                _userRepository.Save(user);
                return RedirectToAction("Index", "Post");
            }
            return View(user);
        }
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null;
            return RedirectToAction("Index","Post");
        }
    }
}