using ApplicationOffice.Common.Api.Cors;
using ApplicationOffice.Common.Api.Tracing;
using ApplicationOffice.Common.Core.Extensions;
using ApplicationOffice.Sso.Core.Services;
using ApplicationOffice.Sso.Data;
using ApplicationOffice.Sso.IdentityServer.Utils;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ApplicationOffice.Sso.IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlobalCors();

            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });
            services
                .AddRouting(options => options.LowercaseUrls = true)
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
            services.AddIS4(Configuration, Environment);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationOffice.Sso.IdentityServer v1"));
                InitializeDatabase(app);
            }

            app.UseRouting();

            app
                .UseExceptionMiddleware()
                .UseTraceIdHeaderMiddleware()
                .UseSerilogRequestLogging();

            app.UseDefaultCors();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
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
