using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Backend.DAL
{
    public class DbContextFactory : IDesignTimeDbContextFactory<SiteDbContext>
    {
        public SiteDbContext CreateDbContext(string[] args)
        {
            
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile($"appsettings.Migrations.json")
                                .Build();
            var connection = configuration.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder().UseSqlite(connection, x => x.MigrationsAssembly("Backend.DAL"))
                                                       .UseLazyLoadingProxies();
            return new SiteDbContext(options.Options);
        }
    }
}