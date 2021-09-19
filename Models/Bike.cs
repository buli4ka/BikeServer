using BikeShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class Bike : BaseModel
    {
        
        public int Price { get; set; }
        public string Description { get; set; }

        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Guid ManufacturerId { get; set; }

        public ICollection<Image> Images { get; set; }


    }
}
