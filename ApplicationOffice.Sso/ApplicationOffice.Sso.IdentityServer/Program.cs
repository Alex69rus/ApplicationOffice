// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using ApplicationOffice.Common.Api.Logging;
using ApplicationOffice.Sso.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

namespace ApplicationOffice.Sso.Is4
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.Title = "ApplicationOffice SSO IdentityServer";

            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var host = CreateHostBuilder(args).Build();

            if (seed)
            {
                Log.Information("Seeding database...");
                var config = host.Services.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString(nameof(SsoDbContext));
                SeedData.EnsureSeedData(connectionString);
                Log.Information("Done seeding database.");
                return 0;
            }

            Log.Information("Starting host...");
            host.Run();
            return 0;
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