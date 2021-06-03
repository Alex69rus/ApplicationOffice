using Microsoft.Extensions.Configuration;

namespace ApplicationOffice.Common.Api.Configurations
{
    public static class ConfigurationExtensions
    {
        public static TConfiguration GetFromNamedSection<TConfiguration>(this IConfiguration configuration)
        {
            return configuration.GetSection(typeof(TConfiguration).Name).Get<TConfiguration>();
        }
    }
}