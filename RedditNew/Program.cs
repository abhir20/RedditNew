using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reddit.AuthTokenRetriever;
using Reddit.AuthTokenRetriever.EventArgs;
using RedditNew.Controllers;
using ServiceLayer;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RedditNew
{
    public class Program
    {

        
        public static void Main(string[] args)
        {
                //Set your appid,refrestoken,accesstoken as environment variables with the keys given below
                string appId = Environment.GetEnvironmentVariable("app_Id");
                string refreshToken = Environment.GetEnvironmentVariable("refresh_token");
                string accessToken = Environment.GetEnvironmentVariable("access_token");

            
           // IRedditClientService redditClient = new RedditClientService(appId,refreshToken, accessToken);




            CreateHostBuilder(args).Build().Run();
        }
    

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



    }
}
