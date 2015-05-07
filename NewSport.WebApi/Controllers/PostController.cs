using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
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
        private readonly ICommentRepository _commentRepository;
        public int PageSize { get; private set; }

        public PostController(IPostRepository postRepository, IUserRepository userRepository,ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            PageSize = 10;
        }


        // GET: Post
        [AllowAnonymous]
        public ViewResult Index(string username, int page = 1)
        {
            
            PostViewModel viewModel = new PostViewModel()
            {
                Posts = _postRepository.Posts.Include(u => u.Author).Where(x => x.Author.Username == username || username == null).OrderByDescending(x => x.Date).Skip((page - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    PostsPerPage = PageSize,
                    TotalPosts = username == null ? _postRepository.Posts.Count() : _postRepository.Posts.Count(x => x.Author.Username == username)
                },
                CurrentUser = username
            };

            return View(viewModel);
        }
        [AllowAnonymous]
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
        [Authorize]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Title,Text,Date")] Post post, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                SaveImageDataIfNotNull(post, image);
                if (_userRepository.IsLogged())
                {
                    post.AuthorId = _userRepository.FindByUsername(Session["user"].ToString()).Id;
                }
                else
                {
                    return new HttpUnauthorizedResult();
                }
                _postRepository.Save(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }


        [HttpGet]
        [Authorize]
        public ViewResult Edit(int? id)
        {
            Post post = _postRepository.FindById(id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Date")]Post post,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                SaveImageDataIfNotNull(post,image);
                _postRepository.Save(post);
                return RedirectToAction("Index");
            }
            return View(post);

        }
        
        private void SaveImageDataIfNotNull(Post post, HttpPostedFileBase image)
        {
            if (image != null)
            {
                post.ImageMimeType = image.ContentType;
                post.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(post.ImageData, 0, image.ContentLength);
            }
        }
        
        [Authorize]
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
                _commentRepository.DeleteByPost(id);
                return Json(post, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }
        [AllowAnonymous]
        public FileContentResult GetImage(int? postId)
        {
            Post post = _postRepository.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                return File(post.ImageData, post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}