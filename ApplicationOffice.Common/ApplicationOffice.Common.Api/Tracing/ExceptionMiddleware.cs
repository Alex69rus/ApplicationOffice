using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationOffice.Common.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ApplicationOffice.Common.Api.Tracing
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (status, msg) = GetStatusCodeWithMessage(ex);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(new ErrorDetails { Message = msg, }.ToString());
        }

        private static (HttpStatusCode, string errorMessage) GetStatusCodeWithMessage(Exception ex) => ex switch
        {
            ValidationException => (HttpStatusCode.BadRequest, ex.Message),
            NotFoundException => (HttpStatusCode.NotFound, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "Internal server error"),
        };

    }

    public class ErrorDetails
    {
        public string Message { get; set; } = default!;

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        ///     Adds default exception handling middleware.
        /// </summary>
        /// <param name="applicationBuilder">Source application builder.</param>
        /// <returns>Original <paramref name="applicationBuilder" />.</returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}