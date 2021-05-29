using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using ApplicationOffice.Common.Api.Cors;
using ApplicationOffice.Common.Api.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ApplicationOffice.Approvals.Api.Tools
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.Audience = "approvals";
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        NameClaimType = "name"
                    };
                    options.RequireHttpsMetadata = false;
                    options.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        SslProtocols = SslProtocols.Tls12,
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                });

            return services;
        }

        public static IServiceCollection AddAPI(this IServiceCollection services)
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
                .AddControllers(cfg => cfg.Filters.Add(new ValidationActionFilter()))
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });

            return services;
        }

        public static IServiceCollection AddApiSwagger(this IServiceCollection services)
        {
            var header = "authorization";

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    header,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = $"Add {header} token",
                        Name = header,
                        Type = SecuritySchemeType.ApiKey,
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = header,
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });

                options.SwaggerDoc("v1", new OpenApiInfo {Title = "ApplicationOffice.Approvals.Api", Version = "v1"});

                var xmlFiles = Directory.GetFiles(
                    AppContext.BaseDirectory,
                    "ApplicationOffice.*.xml",
                    SearchOption.TopDirectoryOnly);

                foreach (var xmlFile in xmlFiles)
                    options.IncludeXmlComments(xmlFile);
            });

            return services;
        }
    }
}
