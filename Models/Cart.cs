using System;
using BikeShop.Models.Base;

namespace BikeShop.Models
{
    public class Cart : BaseModel
    {
        public Guid BikeId { get; set; }
        public Bike Bike { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}