using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewSport.Domain.Api;
using NewSport.Domain.Entity;
using NewSport.WebApi.Controllers;

namespace NewSport.Tests.Controllers
{
    [TestClass]
    public class PostControllerTest
    {
        private PostController _postController;
        private Mock<IPostRepository> _mock;
        [TestInitialize]
        public void Execute()
        {
            _mock = new Mock<IPostRepository>();
            _mock.Setup(repository => repository.Posts).Returns(new List<Post>()
            {
                new Post(){Id = 1,Text = "Loren Ipsum no i co tam",Title = "Dominik Kotecki"},
                new Post(){Id = 2,Text = "Loren Ipsum no i co tam 2",Title = "Dominik Kotecki 1"},
                new Post(){Id = 3,Text = "Loren Ipsum no i co tam 3",Title = "Dominik Kotecki 2"},
                new Post(){Id = 4,Text = "Loren Ipsum no i co tam 4",Title = "Dominik Kotecki 3"},
            }.AsQueryable());
            _postController = new PostController(_mock.Object);    
        }

        [TestMethod]
        public void Index()
        {
            var result = (IQueryable<Post>)_postController.Index().Model;
            Post[] posts = result.ToArray();
            Assert.AreEqual(4,posts.Length);
        }

        [TestMethod]
        public void AddValidData()
        {
            Post post = new Post(){Id = 5,Text = "as",Title = "daadsdsa"};
            ActionResult result = _postController.Add(post);
            _mock.Verify(x=>x.Save(post));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AddInValidData()
        {   
            Post post = new Post() {Title = "daadsdsa" };
            _postController.ModelState.AddModelError("Text", "Text is required");
            ActionResult result = _postController.Add(post);
            _mock.Verify(m=>m.Save(It.IsAny<Post>()),Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
