using Microsoft.AspNetCore.Builder;

namespace ApplicationOffice.Common.Api.Cors
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     Adds a CORS middleware with default policy.
        /// </summary>
        /// <param name="applicationBuilder">Source application builder.</param>
        /// <returns>Original <paramref name="applicationBuilder" />.</returns>
        public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseCors(AoCorsConstants.Cors);
        }
    }
}