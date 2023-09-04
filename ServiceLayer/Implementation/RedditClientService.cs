using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using Reddit.Things;
using RedditService.Features.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class RedditClientService : IRedditClientService
    {
        //   private readonly IMediator _mediator;
        private readonly RedditClient _reddit = null;
        //new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);

        static string appId = "77Vhm2YGgk703wHWBmX9ig";
        static string refreshToken = "760730588485-UfbKjs0RFOSqsyz-_rEG3iHHtgBROg";
        
        static string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzODgxNjU4Ljg3MTY5OCwiaWF0IjoxNjkzNzk1MjU4Ljg3MTY5OCwianRpIjoiYjIwcVhBN09Oa0hOM05LTWNFQXVIeklzODFyaHdnIiwiY2lkIjoiNzdWaG0yWUdnazcwM3dIV0JtWDlpZyIsImxpZCI6InQyXzlwaDNjbXJwIiwiYWlkIjoidDJfOXBoM2NtcnAiLCJsY2EiOjE2MTAwMzUzMTMwMDAsInNjcCI6ImVKeUtWdEpTaWdVRUFBRF9fd056QVNjIiwiZmxvIjo5fQ.kKfiEx7LRNZK5xsOCt2tPeKWEZDJGdqlrtzzFrmPvgRBZV0owCIyCxVouI6_OtwM7oL47AG3IM6puCB7JHZQChTle9YZlZKLpvQ5TEbWBkveZWfGLKEhZNOT-dO6Y3pqwDdcZBvRoSo2TZi5MbT9y48-Ldd1QeBaLWbnkV89RVWIHNA-iKVyYqxIJ9vGhmgLp5SylycotZvuUm8_rqUN0NINuaJC0QUfgt-565etoibMoHcR98rdoXOWNzJeJbJVoE7eqtPFaFewyRaaxXfQd8qSbmdTGk3Xc-O0D8zWAiquXGWhrusGGLO14D7Dt6vSUtsMtvq8jTjzEfe6JZV3kw";

        public RedditClientService()
        {
            _reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);
        }


        public async Task<IList<PostResponse>> GetTopPosts(string name)
        {

            IList<PostResponse> topPostResponse = new List<PostResponse>();
            try
            {
                Reddit.Controllers.Subreddit subName = _reddit.Subreddit(name);
                // Subreddit funny = reddit.Subreddit("funny");

                // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in. 

                //var top = Task.Run()
                //var topPosts =  GetTopPosts(subName);
                var topPosts = subName.Posts.Top.ToList();
              //  Console.WriteLine("Top Posts with the high UpVotes :" + subName.Posts.Top.ToList());

                await Task.Run(() =>
                {
                    foreach (var post in topPosts)
                    {
                        PostResponse tpPost = new PostResponse();
                        tpPost.Fullname = post.Fullname;
                        tpPost.UpVotes = post.UpVotes;
                        tpPost.Author = post.Author;
                        topPostResponse.Add(tpPost);

                    }
                });


                subName.Posts.GetNew();

                //Console.WriteLine("Monitoring subreddit for new posts....");

                //subName.Posts.NewUpdated += C_NewPostsUpdated;
                //subName.Posts.MonitorNew();  // Toggle on.

                //DateTime start = DateTime.Now;
                //while (start.AddMinutes(1) > DateTime.Now) { }

                //// Stop monitoring new posts.  
                //subName.Posts.MonitorNew();  // Toggle off.
                //subName.Posts.NewUpdated -= C_NewPostsUpdated;


                //if (topPostResponse.Count > 0)
                //    return Ok(topPostResponse);
                //else
                //    return NotFound();
                
                    return topPostResponse;

            }

            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task GetUsersWithMostPosts(string name)
        {
            var userPostCounts = new Dictionary<string, int>();

            Reddit.Controllers.Subreddit subName = _reddit.Subreddit(name);

            var topPosts = subName.Posts.Top.ToList();

            await Task.Run(() =>
            {
                foreach (var post in topPosts)
                {
                    var author = post.Author;
                    if (!string.IsNullOrEmpty(author))
                    {
                        if (userPostCounts.ContainsKey(author))
                        {
                            userPostCounts[author]++;
                        }
                        else
                        {
                            userPostCounts[author] = 1;
                        }
                    }
                }

            });

            // Find the user with the most posts
            var userWithMostPosts = userPostCounts.OrderByDescending(x => x.Value).FirstOrDefault();
                if (userWithMostPosts.Key != null)
                {
                   // return userWithMostPosts;
                     Console.WriteLine($"User with the most posts on /r/{name}: {userWithMostPosts.Key} ({userWithMostPosts.Value} posts)");
                }

        }
    }
}
