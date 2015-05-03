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

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        // GET: Comment
        [AllowAnonymous]
        public PartialViewResult Get(int? postId)
        {
            var data = _commentRepository.Comments.Where(x => x.PostId == postId || postId == null).OrderBy(x => x.Id);
            return PartialView(data);
        }

        public ActionResult Add(Comment comment)
        {
            comment.PostId = 1022;
            comment.AuthorId = 8;
            if (Request.IsAjaxRequest())
            {
                 _commentRepository.Save(comment);
                
                var data = _commentRepository.Comments.Select(p => new
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