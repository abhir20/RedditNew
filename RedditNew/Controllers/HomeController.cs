using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedditNew.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        Uri baseAddress = new Uri("https://localhost:5001/api");

        private readonly HttpClient _client;
        public HomeController(ILogger<HomeController> logger)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _logger = logger;
        }


        public IActionResult Index()
        {
            List<PostsModel> postList = new List<PostsModel>();
            var response = _client.GetAsync(_client.BaseAddress + "/posts/GetAllTopPosts");

            return View(response);
        }

        public IActionResult SubredditPosts()
        {
            List<PostsModel> postList = new List<PostsModel>();
            var response = _client.GetAsync(_client.BaseAddress + "/posts/GetAllTopPosts");

            return View(response);
        }
        public IActionResult Privacy()
        {
            List<PostsModel> postList = new List<PostsModel>();
            var response = _client.GetAsync(_client.BaseAddress + "/posts/GetUsersWithTopPosts");
            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
