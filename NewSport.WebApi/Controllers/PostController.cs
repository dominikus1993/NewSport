using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NewSport.Domain.Api;
using NewSport.Domain.Entity;
using NewSport.WebApi.Models;

namespace NewSport.WebApi.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public int PageSize { get; private set; }

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            PageSize = 10;
        }


        // GET: Post
        public ViewResult Index(string username,int page = 1)
        {
            PostViewModel viewModel = new PostViewModel()
            {
                Posts = _postRepository.Posts.Where(x=> x.Author.Username == username || username == null).OrderBy(x=>x.Id).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    PostsPerPage = PageSize,
                    TotalPosts  = username == null?_postRepository.Posts.Count():_postRepository.Posts.Count(x => x.Author.Username == username)
                },
                CurrentUser = username
            };
          
            return View(viewModel);
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
                return HttpNotFound();
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
                post.AuthorId = _userRepository.FindByUsername(Session["user"].ToString()).Id;
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