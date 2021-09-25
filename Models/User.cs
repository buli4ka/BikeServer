using BikeShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        
        pub

        public override string ToString()
        {
           
              return $"Name - {Name}" +
                    $"\n Surname -{Surname}" +
                    $"\n Email- {Email}" +
                    $"\n Password-{Password}";
             
        }
    }
}
