using BikeShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.ViewModels
{
    public class BikeManufacturerViewModel : BaseModel
    {
        public BikeManufacturerViewModel(
            string name,
            string manufacturer,
            int price,
            Guid id
        )
        {
            this.Name = name;
            this.Price = price;
            this.Manufacturer = manufacturer;
            this.Id = id;
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Manufacturer { get; set; }
    }
}
