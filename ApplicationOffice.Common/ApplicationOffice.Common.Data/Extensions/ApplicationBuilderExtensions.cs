using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationOffice.Common.Data.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Automatically rolls out the latest migrations.
        /// </summary>
        /// <param name="applicationBuilder">Source application builder.</param>
        public static void UseAutoMigrations<TDbContext>(this IApplicationBuilder applicationBuilder)
            where TDbContext : DbContext
        {
            using var serviceScope = applicationBuilder
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();

            context?.Database.Migrate();
        }
    }
}