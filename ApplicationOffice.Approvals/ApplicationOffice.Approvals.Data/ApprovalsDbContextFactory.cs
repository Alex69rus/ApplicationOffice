using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ApplicationOffice.Approvals.Data
{
    public class ApprovalsDbContextFactory : IDesignTimeDbContextFactory<ApprovalsDbContext>
    {
        public ApprovalsDbContext CreateDbContext(string[] args)
        {
            if (args.Length == 0)
                args = new[] {"dbmigration.settings.json"};

            return new ApprovalsDbContext(
                new DbContextOptionsBuilder<ApprovalsDbContext>()
                    .UseSqlServer(
                        new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile(args[0])
                            .Build()
                            .GetConnectionString(nameof(ApprovalsDbContext)))
                    .Options);
        }
    }
}
