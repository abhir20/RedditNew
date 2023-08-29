using Microsoft.AspNetCore.Mvc;
using Reddit;
using Reddit.Controllers;
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

        // Initialize the API library instance.  
        RedditClient reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Subreddit funny = reddit.Subreddit("funny");

                // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in. 

                var topPosts = funny.Posts.Top.ToList();
                IList<PostsModel> topPostResponse = new List<PostsModel>();
                Console.WriteLine("Top Posts with the high UpVotes :" + funny.Posts.Top.ToList());

                foreach (var post in topPosts)
                {
                    PostsModel tpPost = new PostsModel();
                    tpPost.Fullname = post.Fullname;
                    tpPost.UpVotes = post.UpVotes;
                    topPostResponse.Add(tpPost);

                }

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

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
