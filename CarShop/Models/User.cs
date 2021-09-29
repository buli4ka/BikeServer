using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Models.Base;

namespace CarShop.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public ICollection<Car> Cars { get; set; }
        
        public Guid CartId { get; set; }

        public override string ToString()
        {
           
              return $"Name - {Name}" +
                    $"\n Surname -{Surname}" +
                    $"\n Email- {Email}" +
                    $"\n Password-{Password}";
             
        }
    }
}
