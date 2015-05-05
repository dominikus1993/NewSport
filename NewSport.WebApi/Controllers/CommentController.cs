using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;
using Newtonsoft.Json;

namespace NewSport.WebApi.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentController(ICommentRepository commentRepository,IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }
        // GET: Comment
        [AllowAnonymous]
        public PartialViewResult Get(int? postId)
        {
            var data = _commentRepository.Comments.Where(x => x.PostId == postId || postId == null).OrderByDescending(x => x.CommentsDate);
            return PartialView(data);
        }

        [Authorize]
        public ActionResult Add(Comment comment,int? postId)
        {
            comment.PostId = postId;
            comment.AuthorId = _userRepository.FindByUsername(Session["user"].ToString()).Id;
            if (Request.IsAjaxRequest())
            {
                 _commentRepository.Save(comment);
                
                var data = _commentRepository.Comments.Where(x=>x.PostId == postId).Select(p => new
                {
                    Id = p.Id,
                    Message = p.Message,
                    CommentsDate = p.CommentsDate,
                    Author = p.Author,

                }).ToList();
                return Json(data,JsonRequestBehavior.AllowGet);
            }
             return PartialView("Get",_commentRepository.Comments as IOrderedQueryable<Comment>);    
            
        }

    }
}