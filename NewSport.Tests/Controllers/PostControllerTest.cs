using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;
using NewSport.Domain.Entity;
using NewSport.WebApi.Controllers;
using NewSport.WebApi.Models;

namespace NewSport.Tests.Controllers
{
    [TestClass]
    public class PostControllerTest
    {
        private PostController _postController;
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private List<Post> _posts;
        private List<User> _users;
        
            
        [TestInitialize]
        public void Execute()
        {
            _posts = new List<Post>()
            {
                new Post() {Id = 1, Text = "Loren Ipsum no i co tam", Title = "Dominik Kotecki",AuthorId = 1},
                new Post() {Id = 2, Text = "Loren Ipsum no i co tam 2", Title = "Dominik Kotecki 1",AuthorId = 1},
                new Post() {Id = 3, Text = "Loren Ipsum no i co tam 3", Title = "Dominik Kotecki 2",AuthorId = 1},
                new Post() {Id = 4, Text = "Loren Ipsum no i co tam 4", Title = "Dominik Kotecki 3",AuthorId = 1}
            };
      
            _users = new List<User>()
            {
                new User(){Id = 1,Email = "Nic@Nic.Nic",Username = "dom109",Password = "admin"}
            };
            
            _postRepositoryMock = new Mock<IPostRepository>();
            _postRepositoryMock.Setup(repository => repository.Posts).Returns(_posts.AsQueryable());
            _postRepositoryMock.Setup(m => m.FindById(4)).Returns(_posts.Find(x => x.Id == 4));

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(repository => repository.Users).Returns(_users.AsQueryable());

            _postController = new PostController(_postRepositoryMock.Object,_userRepositoryMock.Object);    
        }

        [TestMethod]
        public void Index()
        {
            var result = _postController.Index().Model as PostViewModel;
            Assert.AreEqual(4,result.Posts.Count());
        }

        [TestMethod]
        public void AddValidData()
        {
            Post post = new Post(){Id = 5,Text = "as",Title = "daadsdsa"};
            ActionResult result = _postController.Add(post);
            _postRepositoryMock.Verify(x=>x.Save(post));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CanNotEditPost()
        {
            var edit = _postController.Edit(5);
            Post post = edit.ViewData.Model as Post;
            Assert.IsNull(post);
        }

        [TestMethod]
        public void CanEditPost()
        {
            var edit = _postController.Edit(4);
            Post post = edit.ViewData.Model as Post;
            Assert.AreEqual(4,post.Id);
        }

        [TestMethod]
        public void CanNullIdGet()
        {
            var get = _postController.Get(null);
            Assert.IsInstanceOfType(get, typeof(HttpStatusCodeResult));

        }

        [TestMethod]
        public void CanNotGetPost()
        {
            var get = _postController.Get(14);
            Assert.IsInstanceOfType(get,typeof(HttpStatusCodeResult));

        }

        [TestMethod]
        public void CanGetPost()
        {         
            var get = _postController.Get(4) as ViewResult;
            Post post = (Post) get.ViewData.Model;
            Assert.AreEqual(4,post.Id);

        }

        [TestMethod]
        public void AddInValidData()
        {   
            Post post = new Post() {Title = "daadsdsa" };
            _postController.ModelState.AddModelError("Text", "Text is required");
            ActionResult result = _postController.Add(post);
            _postRepositoryMock.Verify(m=>m.Save(It.IsAny<Post>()),Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete()
        {
            var id = 4;
            Post testPost = _posts.Find(x=>x.Id == id);
            var result = _postController.Delete(4);
            _postRepositoryMock.Verify(m=>m.Delete(testPost));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
    }
}
