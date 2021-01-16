using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationOffice.Common.Api.Cors
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add default CORS settings.
        /// </summary>
        /// <param name="services">Service collection to add cors.</param>
        public static IServiceCollection AddGlobalCors(this IServiceCollection services) => services
            .AddCors(options => options
                .AddPolicy(
                    CorsConstants.Cors,
                    builder =>
                     {
                         builder
                             .AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                     }
                )
            );

        /// <summary>
        ///     Add custom CORS settings.
        /// </summary>
        /// <param name="services">Service collection to add cors.</param>
        /// <param name="policyName">Policy name.</param>
        /// <param name="allowedOrigins">Allowed origins (pass empty enumerable or "*" to allow any origin).</param>
        /// <param name="allowedHeaders">Allowed headers (pass empty enumerable or "*" to allow any header).</param>
        /// <param name="allowedMethods">Allowed methods (pass empty enumerable or "*" to allow any method).</param>
        public static IServiceCollection AddCustomCors(
            this IServiceCollection services,
            string policyName,
            IEnumerable<string> allowedOrigins,
            IEnumerable<string> allowedHeaders,
            IEnumerable<string> allowedMethods,
            bool allowCredentials) => services
                .AddCors(options => options
                    .AddPolicy(policyName, builder =>
                     {
                         builder.WithOrigins(
                             allowedOrigins
                                 .ToList()
                                 .IfEmpty(list => list.Add("*"))
                                 .ToArray()
                         );
                         builder.WithHeaders(
                             allowedHeaders
                                 .ToList()
                                 .IfEmpty(list => list.Add("*"))
                                 .ToArray()
                         );
                         builder.WithMethods(
                             allowedMethods
                                 .ToList()
                                 .IfEmpty(list => list.Add("*"))
                                 .ToArray()
                         );
                         if (allowCredentials)
                             builder.AllowCredentials();
                     }
                )
            );
    }
}