using System;

namespace RedditNew.Application.Features.Request
{
    public sealed record TokenRequest
    {
        public string AppId {get;set;}

        public string RefreshToken { get;set;}

        public string AccessToken { get; set; }
    }
}
