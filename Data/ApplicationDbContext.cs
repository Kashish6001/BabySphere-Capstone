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
        }
    }
}