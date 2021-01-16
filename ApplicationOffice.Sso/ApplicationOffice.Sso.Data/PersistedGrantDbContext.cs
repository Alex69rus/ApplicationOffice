using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Sso.Data
{
    public class PersistedGrantDbContext : PersistedGrantDbContext<PersistedGrantDbContext>
    {
        public const string DefaultSchema = "persistedgrant";

        public PersistedGrantDbContext(
            DbContextOptions<PersistedGrantDbContext> options,
            OperationalStoreOptions storeOptions
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
