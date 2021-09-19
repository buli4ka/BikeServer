using BikeShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class Order : BaseModel
    {
     
        public string User { get; set; } 
        public string Address { get; set; } 
        public string ContactPhone { get; set; } 

        public int BicycleId { get; set; } 
        public Bike Bike { get; set; }
    }
}
