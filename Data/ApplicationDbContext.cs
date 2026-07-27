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
        public DbSet<BabyProduct> BabyProducts { get; set; }


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

            modelBuilder.Entity<BabyProduct>().HasData(
                new BabyProduct
                {
                    Id = 1,
                    Name = "Baby Stroller",
                    Category = "Travel",
                    Price = 149.99m,
                    Quantity = 15,
                    Rating = 4.7,
                    Description = "Comfortable stroller for outdoor travel.",
                    ImageUrl = "/images/stroller.jpg"
                },
                new BabyProduct
                {
                    Id = 2,
                    Name = "Baby Car Seat",
                    Category = "Travel",
                    Price = 199.99m,
                    Quantity = 10,
                    Rating = 4.5,
                    Description = "Safe and secure car seat for babies.",
                    ImageUrl = "/images/carseat.jpg"
                },
                new BabyProduct
                {
                    Id = 3,
                    Name = "Diaper Bag",
                    Category = "Travel",
                    Price = 39.99m,
                    Quantity = 19,
                    Rating = 4.9,
                    Description = "Spacious diaper bag for parents.",
                    ImageUrl = "/images/diaperbag.jpg"
                },
                new BabyProduct
                {
                    Id = 4,
                    Name = "Baby Bottle",
                    Category = "Feeding",
                    Price = 12.99m,
                    Quantity = 15,
                    Rating = 4.3,
                    Description = "BPA-free feeding bottle.",
                    ImageUrl = "/images/bottle.jpg"
                },
                new BabyProduct
                {
                    Id = 5,
                    Name = "High Chair",
                    Category = "Feeding",
                    Price = 89.99m,
                    Quantity = 8,
                    Rating = 4.3,
                    Description = "Comfortable feeding chair for babies.",
                    ImageUrl = "/images/highchair.jpg"
                },
                new BabyProduct
                {
                    Id = 6,
                    Name = "Baby Bibs",
                    Category = "Feeding",
                    Price = 9.99m,
                    Quantity = 23,
                    Rating = 3.8,
                    Description = "Soft bibs to keep clothes clean.",
                    ImageUrl = "/images/bibs.jpg"
                },
                new BabyProduct
                {
                    Id = 7,
                    Name = "Baby Lotion",
                    Category = "Care",
                    Price = 8.99m,
                    Quantity = 12,
                    Rating = 4.8,
                    Description = "Gentle lotion for baby skin.",
                    ImageUrl = "/images/lotion.jpg"
                },
                new BabyProduct
                {
                    Id = 8,
                    Name = "Baby Shampoo",
                    Category = "Care",
                    Price = 7.99m,
                    Quantity = 17,
                    Rating = 4.8,
                    Description = "Tear-free shampoo for babies.",
                    ImageUrl = "/images/shampoo.jpg"
                },
                new BabyProduct
                {
                    Id = 9,
                    Name = "Diapers Pack",
                    Category = "Care",
                    Price = 24.99m,
                    Quantity = 20,
                    Rating = 4.6,
                    Description = "Soft and comfortable diapers.",
                    ImageUrl = "/images/diapers.jpg"
                },
                new BabyProduct
                {
                    Id = 10,
                    Name = "Building Blocks",
                    Category = "Toys",
                    Price = 19.99m,
                    Quantity = 23,
                    Rating = 3.7,
                    Description = "Educational blocks for learning.",
                    ImageUrl = "/images/blocks.jpg"
                },
                new BabyProduct
                {
                    Id = 11,
                    Name = "Teddy Bear",
                    Category = "Toys",
                    Price = 14.99m,
                    Quantity = 27,
                    Rating = 3.6,
                    Description = "Soft plush teddy bear.",
                    ImageUrl = "/images/teddy.jpg"
                },
                new BabyProduct
                {
                    Id = 12,
                    Name = "Baby Rattle",
                    Category = "Toys",
                    Price = 6.99m,
                    Quantity = 16,
                    Rating = 4.0,
                    Description = "Colorful rattle toy for babies.",
                    ImageUrl = "/images/rattle.jpg"
                }
            );
        
    }
    }
}