using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
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
        static string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzNzk0Njc5Ljc3NTA3NCwiaWF0IjoxNjkzNzA4Mjc5Ljc3NTA3NCwianRpIjoibnBKVi1uRVFXbFdTTkdqLXdydUNuV0hPNGNzM19nIiwiY2lkIjoiNzdWaG0yWUdnazcwM3dIV0JtWDlpZyIsImxpZCI6InQyXzlwaDNjbXJwIiwiYWlkIjoidDJfOXBoM2NtcnAiLCJsY2EiOjE2MTAwMzUzMTMwMDAsInNjcCI6ImVKeUtWdEpTaWdVRUFBRF9fd056QVNjIiwiZmxvIjo5fQ.UHpxfsGcSHnGphYftyAEIgr7ebRDcgEje0JSkb-ba-Iu88isAFq4qlkcf3ecbWLD1b-0kFtSac82i-0fON4s6UhSM3woTHb2p5-boxQUghFPkQ9_3MZyRckS2xYlNYQXVpotuWfW9d2edzm3p14jwcBr0_rUPTES2XYTIAilMF6xaJJKxQgbPYOPlsxNM5KtJhMVz5acdiAAHeP6a_s-DZ_N5rsUrAW0ZtmnsHZDsLZ1-jjaF5I8BSz407UIU1b49f4WQSO4TgOBcbv_VuUOnBJEq4ou3Z43DmUgkE-J_rBXuLUT8sw-O44mbHNd2S99SopmT84Uu21BFePiM7Eqmg";

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
    }
}
