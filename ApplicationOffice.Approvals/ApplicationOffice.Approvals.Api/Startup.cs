using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Tools;
using ApplicationOffice.Approvals.Data;
using ApplicationOffice.Common.Api.Cors;
using ApplicationOffice.Common.Api.Tracing;
using ApplicationOffice.Common.Core.Extensions;
using ApplicationOffice.Common.Data.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ApplicationOffice.Approvals.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlobalCors()
                .AddRouting(options => options.LowercaseUrls = true)
                .AddApiVersioning(opts =>
                {
                    opts.DefaultApiVersion = new ApiVersion(1, 0);
                    opts.ReportApiVersions = true;
                    opts.AssumeDefaultVersionWhenUnspecified = true;
                })
                .AddVersionedApiExplorer(opts =>
                {
                    opts.GroupNameFormat = "'v'VVV";
                    opts.SubstituteApiVersionInUrl = true;
                })
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ApplicationOffice.Approvals.Api", Version = "v1"});
            });

            services.AddAutoMapper(opt => opt.AddProfile(new CoreMappingProfile()));
            services.AddCore(Configuration);
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.UseSwagger();
            app.UseSwaggerUI(c
                => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationOffice.Approvals.Api v1"));

            app.UseAutoMigrations<ApprovalsDbContext>();

            app.UseRouting();

            app
                .UseExceptionMiddleware()
                .UseTraceIdHeaderMiddleware()
                .UseSerilogRequestLogging()
                .UseDefaultCors()
                .UseSwagger()
                .UseSwaggerUI(options =>
                    provider.ApiVersionDescriptions.ForEach(x =>
                        options.SwaggerEndpoint(
                            $"/swagger/{x.GroupName}/swagger.json",
                            x.GroupName.ToUpperInvariant())));

            app
                .UseAuthentication()
                .UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
