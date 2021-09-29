using System.Collections.Generic;

namespace CarShop.Models.Base
{
    public abstract class CarAttributes : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}