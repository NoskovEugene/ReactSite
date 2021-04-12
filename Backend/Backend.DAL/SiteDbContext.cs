using Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DAL
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext() : base()
        {
        }

        public SiteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new
            {
                Age = 40,
                Email = "admin@mail.com",
                FirstName = "Admin",
                Gender = "Man",
                Id = 1,
                LastName = "Admin",
                Login = "admin",
                MiddleName = "Admin",
                PasswordHash = "21232f297a57a5a743894a0e4a801fc3"
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<News> News { get; set; }
    }
}