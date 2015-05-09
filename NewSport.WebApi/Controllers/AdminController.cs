using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewSport.Domain.Api;

namespace NewSport.WebApi.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public AdminController(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}