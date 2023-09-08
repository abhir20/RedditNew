using Reddit;
using ServiceLayer.Implementation;
using System;

namespace ServiceLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(args);

            string appId = Environment.GetEnvironmentVariable("app_Id");
            string refreshToken = Environment.GetEnvironmentVariable("refresh_token");
            string accessToken = Environment.GetEnvironmentVariable("access_token");


          //  IRedditClientService redditClient = new RedditClientService(appId, refreshToken, accessToken);

        }

    }
}
