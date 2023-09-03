using Microsoft.AspNetCore.Mvc;
using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
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
    public class PostsController : ControllerBase
    {


        private readonly IRedditClientService _redditClientService = null;
        //new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);

       
        public PostsController(IRedditClientService redditClientService)
        {
            _redditClientService = new RedditClientService();
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(string name , CancellationToken cancellationToken)
        {
            string subredditName = name;
            var response = await _redditClientService.GetTopPosts(subredditName);
            return Ok(response);
        }


        //private IList<> GetTopPosts(Subreddit subName)
        //{

        //}

        //public static async void callMethod(string name)
        //{

        //    Subreddit subName = reddit.Subreddit(name);

        //    var topPosts = subName.Posts.Top.ToList();
        //    IList<PostsModel> topPostResponse = new List<PostsModel>();
        //    Console.WriteLine("Top Posts with the high UpVotes :" + subName.Posts.Top.ToList());

        //    await Task.Run(() =>
        //    {
        //        foreach (var post in topPosts)
        //        {
        //            PostsModel tpPost = new PostsModel();
        //            tpPost.Fullname = post.Fullname;
        //            tpPost.UpVotes = post.UpVotes;
        //            tpPost.Author = post.Author;
        //            topPostResponse.Add(tpPost);

        //        }
        //    });


        //}


        //[HttpGet("monitor")]
        //public async void MonitorPosts(string name)
        //{
        //    Subreddit subName = reddit.Subreddit(name);
        //    subName.Posts.GetNew();

        //    Console.WriteLine("Monitoring subreddit for new posts....");

        //    subName.Posts.NewUpdated += C_NewPostsUpdated;
        //    subName.Posts.MonitorNew();  // Toggle on.

        //    DateTime start = DateTime.Now;
        //    while (start.AddMinutes(1) > DateTime.Now) { }

        //    // Stop monitoring new posts.  --Kris
        //    subName.Posts.MonitorNew();  // Toggle off.
        //    subName.Posts.NewUpdated -= C_NewPostsUpdated;

        //}

        /// <summary>
        /// Custom event handler for handling monitored new posts as they come in.
        /// See Reddit.NETTests for more complex examples.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
        {
            foreach (Post post in e.Added)
            {
                Console.WriteLine("[" + post.Subreddit + "] New Post by " + post.Author + ": " + post.Title);
            }
        }

        /// <summary>
        /// Custom event handler for handling monitored new comments as they come in.
        /// See Reddit.NETTests for more complex examples.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                Console.WriteLine("[" + comment.Subreddit + "/" + comment.Root.Title + "] New Comment by " + comment.Author + ": " + comment.Body);
            }
        }
    }

}

