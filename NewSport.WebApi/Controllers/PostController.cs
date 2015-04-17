﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewSport.Domain.Api;

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
    }
}