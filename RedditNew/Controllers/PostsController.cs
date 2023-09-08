using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using RedditNew.Application.Features.Request;
using RedditNew.Models;
using ServiceLayer;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController :  Controller
    {


        private readonly IRedditClientService _redditClientService = null;
        //private readonly IRepository<Posts> _postRepository; -- Need to be added when we scale up the application to use database.
        
        public PostsController(IRedditClientService redditClientService)
        {
            _redditClientService = redditClientService;
            //_postRepository = postRepository; -- Need to be used when the IRepository is injected
        }



        [HttpGet]
        public async Task<ActionResult> GetAllTopPosts(string name , CancellationToken cancellationToken)
        {
            string subredditName = name;
            var response = await _redditClientService.GetTopPosts(subredditName);
            return Ok(response);
        }

        [HttpGet("User")]
        public async Task<ActionResult> GetUsersWithTopPosts(string name, CancellationToken cancellationToken)
        {
            string subredditName = name;
            var response = await _redditClientService.GetUsersWithMostPosts(subredditName);
            return Ok(response);
            
        }


     
    }

}

