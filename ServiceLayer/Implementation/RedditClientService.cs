using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using RedditNew.Application.Features.Response;
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
        
        static string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzNzAzODgzLjkwNDQ0OSwiaWF0IjoxNjkzNjE3NDgzLjkwNDQ0OSwianRpIjoiYzVqeVlvZ0xtVUxQOVJYVTBFbmpiUnBFc0VNdzJnIiwiY2lkIjoiNzdWaG0yWUdnazcwM3dIV0JtWDlpZyIsImxpZCI6InQyXzlwaDNjbXJwIiwiYWlkIjoidDJfOXBoM2NtcnAiLCJsY2EiOjE2MTAwMzUzMTMwMDAsInNjcCI6ImVKeUtWdEpTaWdVRUFBRF9fd056QVNjIiwiZmxvIjo5fQ.Wfz14dc05a1cD2-vhCk8m_CmQtQ1g0HDRPmd__aUO42MEW6M9HRm3Dk4vmJDYnxKWuOUF5po1sn9Mv2SbEneoRmal7yPvqcejxAfxwAVkueLR5-Ws7R3FDHSwlPRfza7HnodPn3jfwCMLUttGHWJ-57eOVMNLNWkVensLjGEyewskiavC8TLdHzVr6ZdHeP3EGzD5VyhxbeLdDUUuksYPQwL8PNPwRlo5DjSni5_9eZL02Ub3SLvPYphfNRPYe5537jif6PKX_nrnkB4x7K3X1Dq5iItQAXNMFmLqq_vMNeJqcbOB8BP9jIfJPkXY3zQjYqiJ10aZFSPJeSxTvrXqg";

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
                Console.WriteLine("Top Posts with the high UpVotes :" + subName.Posts.Top.ToList());

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

                Console.WriteLine("Monitoring subreddit for new posts....");

                //subName.Posts.NewUpdated += C_NewPostsUpdated;
                //subName.Posts.MonitorNew();  // Toggle on.

                //DateTime start = DateTime.Now;
                //while (start.AddMinutes(1) > DateTime.Now) { }

                //// Stop monitoring new posts.  --Kris
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
                throw ex;
            }
        }
    }
}
