using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using CarShop.Models.CarAttributes;


namespace CarShop.Database
{
    public class ShopContext
        : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Cart> Cart { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comfort> Comforts { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<WheelDrive> WheelDrives { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }




        
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}