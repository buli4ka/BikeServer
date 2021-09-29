using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Models.Base;

namespace CarShop.Models
{
    public class Order : BaseModel
    {
     
        public string User { get; set; } 
        public string Address { get; set; } 
        public string ContactPhone { get; set; } 

        public int BicycleId { get; set; } 
        public Car Car { get; set; }
    }
}
