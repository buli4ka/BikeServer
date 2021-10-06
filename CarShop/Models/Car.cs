using System;
using System.Collections.Generic;
using CarShop.Models.Base;
using CarShop.Models.CarAttributes;

namespace CarShop.Models
{
    public class Car : BaseModel
    {
        public int Price { get; set; }
        public string Description { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Guid ManufacturerId { get; set; }


        public Engine Engine { get; set; }
        public Guid EngineId { get; set; }

        public Transmission Transmission { get; set; }
        public Guid TransmissionId { get; set; }

        public WheelDrive WheelDrive { get; set; }
        public Guid WheelDriveId { get; set; }


        public ICollection<Comfort> Comfort { get; set; }

        public ICollection<Security> Securities { get; set; }
        public ICollection<Image> Images { get; set; }

        public ICollection<User> Users { get; set; }
    }
}