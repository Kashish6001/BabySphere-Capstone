using BabySphere.Models;
using Microsoft.EntityFrameworkCore;

namespace BabySphere.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Babysitter> Babysitters { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ParentProfile> ParentProfiles { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Email = "admin@babysphere.com",
                    Password = "admin123"
                }
            );

            modelBuilder.Entity<UserAccount>().HasData(
    new UserAccount
    {
        Id = 1,
        FullName = "BabySphere Administrator",
        Email = "admin@babysphere.com",
        Password = "admin123",
        Role = "Admin"
    },
    new UserAccount
    {
        Id = 2,
        FullName = "Test Parent",
        Email = "parent@babysphere.com",
        Password = "parent123",
        Role = "Parent"
    },
    new UserAccount
    {
        Id = 3,
        FullName = "Test Babysitter",
        Email = "babysitter@babysphere.com",
        Password = "babysitter123",
        Role = "Babysitter"
    }
);
        }
    }
}