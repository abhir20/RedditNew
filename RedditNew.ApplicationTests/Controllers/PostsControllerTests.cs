using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void GetAllTopPosts_ShouldReturnAllTopPosts()
        {
            RedditClientService redditCliemtService = new RedditClientService();
            var controller = new PostsController(redditCliemtService);

            var result = controller.GetAll("funny",default);
            Assert.AreEqual(result, typeof(NotFoundResult));


            //var testPosts = GetTestProducts();
            //var controller = new SimpleProductController(testProducts);

            //var result = controller.GetAllProducts() as List<Product>;
            //Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}