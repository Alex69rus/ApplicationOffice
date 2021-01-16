using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Sso.Data
{
    public class ConfigurationDbContext : ConfigurationDbContext<ConfigurationDbContext>
    {
        public const string DefaultSchema = "configurations";

        public ConfigurationDbContext(
            DbContextOptions<ConfigurationDbContext> options,
            ConfigurationStoreOptions storeOptions
        )
            : base(options, storeOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
