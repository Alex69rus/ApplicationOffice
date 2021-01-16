using System;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace ApplicationOffice.Common.Api.Logging
{
    public static class HostBuilderLoggingExtensions
    {
        public static readonly string DefaultOutput = "{Timestamp:MM-dd HH:mm:ss.fff zzz} " +
                                                      "[{TraceId}] " +
                                                      "[{Level:u3}] " +
                                                      "{SourceContext}: " +
                                                      "{Message:lj}{NewLine}{Exception}";

        private static readonly string ExecutingAssembly = Assembly.GetEntryAssembly()!.GetName()!.Name!;

        /// <summary>
        /// Adds Serilog logging with the default configuration and possibility to extend it.
        /// </summary>
        /// <param name="hostBuilder">Source host builder.</param>
        /// <param name="configure">Custom configuration action.</param>
        /// <returns>Source host builder.</returns>
        public static IHostBuilder UseLogging(
            this IHostBuilder hostBuilder,
            Action<HostBuilderContext, LoggerConfiguration>? configure = null
        )
        {
            return hostBuilder
                .UseSerilog(
                    (hostBuilderContext, configuration) =>
                    {
                        configuration.MinimumLevel.Information();
                        configuration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
                        configuration.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);

                        configuration.Enrich.FromLogContext();
                        configuration.Enrich.WithProperty("app", ExecutingAssembly);

                        configuration.WriteTo.Console(outputTemplate: DefaultOutput);

                        configuration.ReadFrom.Configuration(hostBuilderContext.Configuration);

                        configure?.Invoke(hostBuilderContext, configuration);
                    }
                );
        }

        /// <summary>
        /// Adds Serilog logging in JSON format with the default configuration.
        /// </summary>
        /// <param name="hostBuilder">Source host builder.</param>
        /// <returns>Source host builder.</returns>
        public static IHostBuilder UseJsonLogging(
            this IHostBuilder hostBuilder,
            Action<HostBuilderContext, LoggerConfiguration>? configure = null
        )
        {
            return hostBuilder
                .UseSerilog(
                    (hostBuilderContext, configuration) =>
                    {
                        configuration.MinimumLevel.Information();
                        configuration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
                        configuration.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);

                        configuration.Enrich.FromLogContext();
                        configuration.Enrich.WithProperty("app", ExecutingAssembly);

                        configuration.WriteTo.Console(new JsonFormatter());

                        configuration.ReadFrom.Configuration(hostBuilderContext.Configuration);

                        configure?.Invoke(hostBuilderContext, configuration);
                    }
                );
        }
    }
}
