using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Models.Base;

namespace CarShop.Models.CarAttributes
{
    public class Image : BaseModel
    {
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}