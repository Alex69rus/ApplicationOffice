using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationOffice.Approvals.Data.Tools
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApprovalsDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStr = configuration.GetConnectionString(nameof(ApprovalsDbContext));
            if (connectionStr == null)
                throw new Exception("Connection string for ApprovalsDbContext is null");

            services.AddDbContext<ApprovalsDbContext>(opt => opt.UseSqlServer(connectionStr));

            return services;
        }
    }
}
