using Reddit.Controllers.Structures;
using Reddit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditService.Features.Response
{
    public sealed record PostResponse
    {
        //
        // Summary:
        //     The username of the post author.
        public string Author { get; set; }
        //
        // Summary:
        //     The ID36 of the post.
        public string Id { get; set; }
        //
        // Summary:
        //     The fullname of the post.
        public string Fullname { get; set; }
        //
        // Summary:
        //     The permalink URL of the post.
        public string Permalink { get; set; }
        //
        // Summary:
        //     When the post was created.
        public DateTime Created { get; set; }
        //
        // Summary:
        //     When the post was last edited.
        public DateTime Edited { get; set; }
        //
        // Summary:
        //     Whether the post was removed.
        public bool Removed { get; set; }
        //
        // Summary:
        //     Whether the post was marked as spam.
        public bool Spam { get; set; }
        //
        // Summary:
        //     Whether the post was marked as NSFW.
        public bool NSFW { get; set; }
        //
        // Summary:
        //     The number of upvotes received divided by the total number of votes.
        public double UpvoteRatio { get; set; }
        //
        // Summary:
        //     The number of upvotes received.
        public int UpVotes { get; set; }
        //
        // Summary:
        //     The subreddit in which the post exists.
        public string Subreddit { get; set; }
        //
        // Summary:
        //     Whether the post has been upvoted by the authenticated user.
        public bool IsUpvoted { get; }
        //
        // Summary:
        //     Whether the post has been downvoted by the authenticated user.
        public bool IsDownvoted { get; }
        //
        // Summary:
        //     Any awards applied to the post.
        public Awards Awards { get; set; }
        //
        // Summary:
        //     The number of downvotes received.
        public int DownVotes { get; }
        //
        // Summary:
        //     The title of the post.
        public string Title { get; set; }
        //
        // Summary:
        //     The full Listing object returned by the Reddit API;
        public Reddit.Things.Post Listing { get; set; }
        //
        // Summary:
        //     Comment replies to this post.
        public Comments Comments { get; }
        //
        // Summary:
        //     The post score.
        public int Score { get; set; }
    }
}
