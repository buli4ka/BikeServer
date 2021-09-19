using BikeShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models.SampleData
{
    public class SampleData
    {
        public static void Initialize(ShopContext context)
        {
            if (!context.Bikes.Any())
            {

                context.Users.AddRange(
                    new User
                    {
                        Email = "admin",
                        Password = "admin",
                        Role = "admin",
                        Name = "admin",
                        Surname = "admin"
                    }, new User
                    {
                        Email = "user",
                        Password = "user",
                        Role = "user",
                        Name = "user",
                        Surname = "user"
                    });

                context.SaveChanges();
            }


        }
    }
}
