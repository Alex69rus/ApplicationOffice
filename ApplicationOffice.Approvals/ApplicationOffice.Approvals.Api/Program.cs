using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Common.Api.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApplicationOffice.Approvals.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "ApplicationOffice Approvals API";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseJsonLogging();
    }
}
