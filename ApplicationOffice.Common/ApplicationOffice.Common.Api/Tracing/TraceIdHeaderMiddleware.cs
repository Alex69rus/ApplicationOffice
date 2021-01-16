using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ApplicationOffice.Common.Api.Tracing
{
    public class TraceIdHeaderMiddleware
    {
        public const string TraceIdHeader = "x-trace-id";

        private readonly RequestDelegate _next;

        public TraceIdHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var traceId = Activity.Current?.Id?.ToString() ?? Guid.NewGuid().ToString();

            context.Response.OnStarting(
                state =>
                {
                    (state as HttpContext)?.Response.Headers.Add(TraceIdHeader, traceId);
                    return Task.CompletedTask;
                },
                context
            );

            await _next.Invoke(context);
        }
    }

    public static class TraceIdHeaderMiddlewareExtensions
    {
        /// <summary>
        ///     Adds default request tracing middleware.
        /// </summary>
        /// <param name="applicationBuilder">Source application builder.</param>
        /// <returns>Original <paramref name="applicationBuilder" />.</returns>
        public static IApplicationBuilder UseTraceIdHeaderMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<TraceIdHeaderMiddleware>();
        }
    }
}