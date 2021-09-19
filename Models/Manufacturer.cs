using BikeShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class Manufacturer : BaseModel 
    {
    
        public string Name { get; set; }
        public ICollection<Bike> Bikes { get; set; }
       
    }
}
