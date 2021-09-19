using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
