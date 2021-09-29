using System;
using CarShop.Models.Base;

namespace CarShop.Models
{
    public class Cart : BaseModel
    {
        public Guid BikeId { get; set; }
        public Car Car { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}