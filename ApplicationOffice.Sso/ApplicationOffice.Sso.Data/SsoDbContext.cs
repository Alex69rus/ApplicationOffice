using ApplicationOffice.Sso.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Sso.Data
{
    public class SsoDbContext : IdentityDbContext<AoIdentityUser, AoIdentityRole, string>
    {
        public const string DefaultSchema = "sso";

        public SsoDbContext(DbContextOptions<SsoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(DefaultSchema);

            builder.Entity<AoIdentityUser>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}
