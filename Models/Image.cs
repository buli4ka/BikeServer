using BikeShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class Image : BaseModel
    {
       
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }


        public Guid BikeId { get; set; }
        public Bike Bike { get; set; }
    }
}
