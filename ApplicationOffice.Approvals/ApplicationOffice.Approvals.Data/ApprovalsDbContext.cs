using ApplicationOffice.Approvals.Data.Entities;
using ApplicationOffice.Approvals.Data.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Approvals.Data
{
    public class ApprovalsDbContext : DbContext
    {
        public DbSet<Application> Applications { get; protected set; } = default!;
        public DbSet<ApplicationApprover> ApplicationApprovers { get; protected set; } = default!;
        public DbSet<ApplicationField> ApplicationFields { get; protected set; } = default!;
        public DbSet<Unit> Units { get; protected set; } = default!;
        public DbSet<UnitApprover> UnitApprovers { get; protected set; } = default!;
        public DbSet<User> Users { get; protected set; } = default!;

        public ApprovalsDbContext(DbContextOptions<ApprovalsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationApproverConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationFieldConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new UnitApproverConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
