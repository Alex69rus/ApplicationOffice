using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApplicationOffice.Sso.Data
{
    internal class FactoriesConst
    {
        public const string ConnectionString =
            "Server=localhost,1433;Database=application-office_sso;User Id=sa;Password=Password1@;";
    }

    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();

            optionsBuilder.UseSqlServer(
                FactoriesConst.ConnectionString,
                x => x
                    .CommandTimeout(3600)
                    .MigrationsAssembly(typeof(PersistedGrantDbContextFactory).Assembly.GetName().Name)
            );

            var is4Opts = new ConfigurationStoreOptions
            {
                ConfigureDbContext = x => x.UseSqlServer(
                    FactoriesConst.ConnectionString,
                    x => x.MigrationsAssembly(typeof(ConfigurationDbContextFactory).Assembly.GetName().Name)
                ),
                DefaultSchema = ConfigurationDbContext.DefaultSchema,
            };

            return new ConfigurationDbContext(optionsBuilder.Options, is4Opts);
        }
    }

    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();

            optionsBuilder.UseSqlServer(
                FactoriesConst.ConnectionString,
                x => x
                    .CommandTimeout(3600)
                    .MigrationsAssembly(typeof(PersistedGrantDbContextFactory).Assembly.GetName().Name)
            );

            var is4Opts = new OperationalStoreOptions
            {
                ConfigureDbContext = x => x.UseSqlServer(
                    FactoriesConst.ConnectionString,
                    x => x.MigrationsAssembly(typeof(PersistedGrantDbContextFactory).Assembly.GetName().Name)
                ),
                DefaultSchema = PersistedGrantDbContext.DefaultSchema,
            };

            return new PersistedGrantDbContext(optionsBuilder.Options, is4Opts);
        }
    }

    public class SsoDbContextFactory : IDesignTimeDbContextFactory<SsoDbContext>
    {
        public SsoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SsoDbContext>();

            optionsBuilder.UseSqlServer(
                FactoriesConst.ConnectionString,
                x => x
                    .CommandTimeout(3600)
                    .MigrationsAssembly(typeof(SsoDbContextFactory).Assembly.GetName().Name)
            );

            return new SsoDbContext(optionsBuilder.Options);
        }
    }
}
