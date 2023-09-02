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

        //Method implementation to be done on service layer and called asynchronously

        private readonly IRedditClientService _redditClientService = null;
        //new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);


        static string appId = "77Vhm2YGgk703wHWBmX9ig";
        static string refreshToken = "760730588485-UfbKjs0RFOSqsyz-_rEG3iHHtgBROg";
        static string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzNTQ0NjkzLjg3MjYsImlhdCI6MTY5MzQ1ODI5My44NzI2LCJqdGkiOiJEb2FOYUtXXzZRVURFT3hjMnVqdUV1UUpPRFJKbHciLCJjaWQiOiI3N1ZobTJZR2drNzAzd0hXQm1YOWlnIiwibGlkIjoidDJfOXBoM2NtcnAiLCJhaWQiOiJ0Ml85cGgzY21ycCIsImxjYSI6MTYxMDAzNTMxMzAwMCwic2NwIjoiZUp5S1Z0SlNpZ1VFQUFEX193TnpBU2MiLCJmbG8iOjl9.cIzdZ2QUmdUniLzf9fYpl-jJS6S2vF-ru3tt_2Le6OvptojGD3cBmn0EnUMr5_P0Iu4CMhcJpyCAQ3Xvivgz7A2WypNnogNejsD65tli2nVkN4QvrD_pNepvtY6r9q25fPvP2piF8mPYDNqrnODsvVargq4XLUYLLTfr3YGav0KC0fZmZx7dTSGGYuujROClP-AFhto3iIav5QCRpbhvXZZs5idLU9nRof3Fik9ToNSGwP34RYFqnX72sH1e8st9kCDY4w5uKgfsn0p1v4Rd0lvS9gANDuY-TLOA5XPQt5PUo7W7fGQgye7JIIhQ62-vOPmZSjowoQcXbKYl5vhXTQ";

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

