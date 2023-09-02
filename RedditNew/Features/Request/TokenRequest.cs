using System;

namespace RedditNew.Application.Features.Request
{
    public sealed record TokenRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
