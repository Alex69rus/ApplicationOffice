using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Services;
using ApplicationOffice.Approvals.Data.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationOffice.Approvals.Core.Tools
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApprovalsDbContext(configuration);

            services.AddScoped<IApplicationService, ApplicationService>();

            return services;
        }
    }
}
