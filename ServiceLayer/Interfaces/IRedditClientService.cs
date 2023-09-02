using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using Reddit.Things;
using RedditNew.Application.Features.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IRedditClientService
    {
        public Task<IList<PostResponse>> GetTopPosts(string name);

        //Task<Post> GetByEmail(string email, CancellationToken cancellationToken);


    }
}
