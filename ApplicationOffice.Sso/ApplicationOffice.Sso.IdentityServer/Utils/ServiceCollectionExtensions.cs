using System;
using ApplicationOffice.Sso.Data;
using ApplicationOffice.Sso.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationOffice.Sso.IdentityServer.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIS4(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString(nameof(SsoDbContext));

            services.AddDbContext<SsoDbContext>(opts => opts.UseSqlServer(connectionString));

            services
                .AddIdentity<AoIdentityUser, AoIdentityRole>()
                .AddEntityFrameworkStores<SsoDbContext>()
                .AddDefaultTokenProviders();

            var builder = services
                .AddIdentityServer(
                    options =>
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;

                        options.EmitStaticAudienceClaim = true;
                    }
                )
                .AddConfigurationStore<ConfigurationDbContext>(
                    options =>
                    {
                        options.DefaultSchema = ConfigurationDbContext.DefaultSchema;
                        options.ConfigureDbContext = optsBuilder => optsBuilder.UseSqlServer(
                            connectionString,
                            sql => sql.CommandTimeout(3600)
                        );
                    }
                )
                .AddOperationalStore<PersistedGrantDbContext>(
                    options =>
                    {
                        options.DefaultSchema = PersistedGrantDbContext.DefaultSchema;
                        options.ConfigureDbContext = optsBuilder => optsBuilder.UseSqlServer(
                            connectionString,
                            sql => sql.CommandTimeout(3600)
                        );
                    }
                )
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            if (!env.IsProduction())
                builder.AddDeveloperSigningCredential();

            return services;
        }
    }
}