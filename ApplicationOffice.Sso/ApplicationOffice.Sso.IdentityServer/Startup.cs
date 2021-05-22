// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationOffice.Common.Api.Cors;
using ApplicationOffice.Sso.IdentityServer.Tools;
using ApplicationOffice.Sso.Data;
using ApplicationOffice.Common.Core.Extensions;
using IdentityServer4.EntityFramework.Mappers;
using ApplicationOffice.Common.Data.Extensions;
using ApplicationOffice.Common.Api.Tracing;
using Serilog;
using ApplicationOffice.Sso.Core.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace ApplicationOffice.Sso.Is4
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlobalCors();

            services
                .AddControllersWithViews()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // options.SuppressConsumesConstraintForFormFileParameters = true;
                    // options.SuppressInferBindingSourcesForParameters = true;
                    // options.SuppressModelStateInvalidFilter = true;
                    // options.SuppressMapClientErrors = true;
                });
            services
                .AddRouting(/*options => options.LowercaseUrls = true*/)
                .AddApiVersioning(options => options.ReportApiVersions = true)
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApplicationOffice.Sso.IdentityServer", Version = "v1" });
            });

            services.TryAddScoped<IUserService, UserService>();
            services.AddAuthentication();
            services.AddIS4(Configuration, Environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationOffice.Sso.IdentityServer v1"));
            InitializeDatabase(app);

            app.UseAutoMigrations<ConfigurationDbContext>();
            app.UseAutoMigrations<PersistedGrantDbContext>();
            app.UseAutoMigrations<SsoDbContext>();

            app.UseStaticFiles();

            app
                .UseRouting()
                .UseExceptionMiddleware()
                .UseTraceIdHeaderMiddleware()
                .UseSerilogRequestLogging();

            app.UseDefaultCors();

            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Clients.RemoveRange(context.Clients);
            Config.Clients.ForEach(x => context.Clients.Add(x.ToEntity()));

            context.ApiScopes.RemoveRange(context.ApiScopes);
            Config.ApiScopes.ForEach(x => context.ApiScopes.Add(x.ToEntity()));

            context.ApiResources.RemoveRange(context.ApiResources);
            Config.ApiResources.ForEach(x => context.ApiResources.Add(x.ToEntity()));

            context.SaveChanges();
        }
    }
}