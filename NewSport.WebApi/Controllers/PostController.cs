﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NewSport.Domain.Api;
using NewSport.Domain.Entity;

namespace NewSport.WebApi.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Post
        public ViewResult Index()
        {
            var model = _postRepository.Posts.OrderBy(p => p.Id);
            return View(model);
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
                _postRepository.Save(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Int32 id)
        {
            Post post = _postRepository.Posts.First(x => x.Id == id);
            if (post != null)
            {
                _postRepository.Delete(post);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}