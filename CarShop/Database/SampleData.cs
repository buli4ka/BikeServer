using System.Linq;
using CarShop.Models.CarAttributes;

namespace CarShop.Database
{
    public class SampleData
    {
        public static void Initialize(ShopContext context)
        {
            if (!context.Cars.Any())
            {
                context.Manufacturers.AddRange(new Manufacturer
                {
                    Name = "Dodge"
                },
                    new Manufacturer {
                    Name = "Toyota"
                });
                context.Engines.AddRange(
                    new Engine
                    {
                        Name = "Hemmi"
                    },
                    new Engine
                    {
                        Name = "2JZ"
                    });
                context.Transmissions.AddRange(
                    new Transmission
                    {
                        Name = "Automatic"
                    },
                    new Transmission
                    {
                        Name = "Mechanical"
                    },
                    new Transmission
                    {
                        Name = "Robot"
                    }
                );
                context.WheelDrives.AddRange(
                    new WheelDrive
                    {
                        Name = "Back"
                    },
                    new WheelDrive
                    {
                        Name = "Front"
                    },
                    new WheelDrive
                    {
                        Name = "Full"
                    }
                );
                
                context.Securities.AddRange(
                    new Security
                    {
                        Name = "Door Lock"
                    },
                    new Security
                    {
                        Name = "Window Lock"
                    },
                    new Security
                    {
                        Name = "Alarm"
                    }
                );
                
                context.Comforts.AddRange(
                    new Comfort
                    {
                        Name = "Window"
                    },
                    new Comfort
                    {
                        Name = "Door"
                    },
                    new Comfort
                    {
                        Name = "Seat"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}