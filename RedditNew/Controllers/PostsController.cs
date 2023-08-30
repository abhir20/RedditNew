using Microsoft.AspNetCore.Mvc;
using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using RedditNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {


        static string appId = "77Vhm2YGgk703wHWBmX9ig";
        static string refreshToken = "760730588485-UfbKjs0RFOSqsyz-_rEG3iHHtgBROg";
        static string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzNDI2MTE3Ljk5NzUwNSwiaWF0IjoxNjkzMzM5NzE3Ljk5NzUwNSwianRpIjoiQmFkbDNiZDF4RUlQdkRpcHVZOGNoZUtzeWJvdS1BIiwiY2lkIjoiNzdWaG0yWUdnazcwM3dIV0JtWDlpZyIsImxpZCI6InQyXzlwaDNjbXJwIiwiYWlkIjoidDJfOXBoM2NtcnAiLCJsY2EiOjE2MTAwMzUzMTMwMDAsInNjcCI6ImVKeUtWdEpTaWdVRUFBRF9fd056QVNjIiwiZmxvIjo5fQ.kkMKzKh7BtOc9rnjWPn-kurRvU_fgD2U4KBVCePxuiX8YgTzYpBOEUOWp2dNsMLSPxzfK6z5ZOmPsr92Z4fVweDzvUPtG9jN-YvUkVy8EUm-CPGo6SzLBjzGpGOM69SI_yxlwrA1vWAluyv9bOaxALJTiO5AAtyC3mlhBdW9V-AYEr_cnCyivN9rYP4ilfNDBYKRuKN31BSLkg3uDfEcFGQ-df4djhbUyvEJtYyM2h2_geqJB9FBzzsZE7ETglsN8GNSohcRXjBJkULqnKbk92HQwoBmVxPjQHJzf-smwWK7JuKgZZAKExmtMFz3r2nCac33QRmzBuyZMzS8W2TZ-Q";

        private readonly RedditClient reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);
       

        // Initialize the API library instance.  
       // RedditClient reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                Subreddit subName = reddit.Subreddit(name);
                Subreddit funny = reddit.Subreddit("funny");

                // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in. 

                var topPosts = subName.Posts.Top.ToList();
                IList<PostsModel> topPostResponse = new List<PostsModel>();
                Console.WriteLine("Top Posts with the high UpVotes :" + subName.Posts.Top.ToList());

                foreach (var post in topPosts)
                {
                    PostsModel tpPost = new PostsModel();
                    tpPost.Fullname = post.Fullname;
                    tpPost.UpVotes = post.UpVotes;
                    tpPost.Author = post.Author;
                    topPostResponse.Add(tpPost);

                }
                subName.Posts.GetNew();

                Console.WriteLine("Monitoring funny for new posts....");

                subName.Posts.NewUpdated += C_NewPostsUpdated;
                subName.Posts.MonitorNew();  // Toggle on.

                DateTime start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                // Stop monitoring new posts.  --Kris
                subName.Posts.MonitorNew();  // Toggle off.
                subName.Posts.NewUpdated -= C_NewPostsUpdated;


                if (topPostResponse.Count > 0)
                    return Ok(topPostResponse);
                else
                    return NotFound();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ////To get the Users with most posts
        //[HttpGet("author")]
        //public async Task<IActionResult> GetUserWithMostPosts(string name)
        //{
        //    try
        //    {
        //        Subreddit subName = reddit.Subreddit(name);
        //        Subreddit funny = 


        //        // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in. 


        //        var allPosts = reddit.SearchUsers.;

        //        var usersWithMostPosts = allPosts.Lists;
        //      //  IList<PostsModel> topPostResponse = new List<PostsModel>();
        //        //Console.WriteLine("Top Posts with the high UpVotes :" + subName.Posts.Top.ToList());

                
        //            return Ok(allPosts);
        //        //else
        //        //    return NotFound();
        //    }

        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
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

