using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditNew.Application.Features.Response;
using RedditNew.ApplicationTests;
using RedditNew.Controllers;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditNew.Controllers.Tests
{
    [TestClass()]
    public class PostsControllerTests 
    {
        [TestMethod]
        public async Task GetAllTopPosts_ShouldReturnAllTopPosts()
        {
            RedditClientService redditCliemtService = new RedditClientService();
            var controller = new PostsController(redditCliemtService);

            var result = await controller.GetAllTopPosts("funny",default);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

    }
}