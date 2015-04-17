using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestInitialize]
        public void Execute()
        {
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(repository => repository.Posts).Returns(new List<Post>()
            {
                new Post(){Id = 1,Text = "Loren Ipsum no i co tam",Title = "Dominik Kotecki"},
                new Post(){Id = 2,Text = "Loren Ipsum no i co tam 2",Title = "Dominik Kotecki 1"},
                new Post(){Id = 3,Text = "Loren Ipsum no i co tam 3",Title = "Dominik Kotecki 2"},
                new Post(){Id = 4,Text = "Loren Ipsum no i co tam 4",Title = "Dominik Kotecki 3"},
            }.AsQueryable());
            _postController = new PostController(mock.Object);    
        }

        [TestMethod]
        public void Index()
        {
            var result = (IQueryable<Post>)_postController.Index().Model;
            Post[] posts = result.ToArray();
            Assert.AreEqual(4,posts.Length);
        }
    }
}
