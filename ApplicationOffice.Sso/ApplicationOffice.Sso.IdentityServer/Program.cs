using ApplicationOffice.Common.Api.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace ApplicationOffice.Sso.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "ApplicationOffice SSO IdentityServer";

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseJsonLogging();
    }
}