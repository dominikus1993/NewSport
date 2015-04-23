﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NewSport.Domain.Api;
using NewSport.Domain.Entity;

namespace NewSport.WebApi.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }


        // GET: Post
        public ViewResult Index()
        {
            var model = _postRepository.Posts.ToList();
            return View(model);
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.FindById(id);
            if (post == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(post);
        }

        //GET: Post
        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Title,Text,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = 1;
                _postRepository.Save(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpGet]
        public ViewResult Edit(int? id)
        {
            Post post = _postRepository.FindById(id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Date")]Post post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Save(post);
                return RedirectToAction("Index");
            }
            return View(post);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.FindById(id);
            if (post != null)
            {
                _postRepository.Delete(post);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

    }
}