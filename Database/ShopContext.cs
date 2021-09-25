using Microsoft.EntityFrameworkCore;
using BikeShop.Models;


namespace BikeShop.Database
{
    public class ShopContext
        : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}