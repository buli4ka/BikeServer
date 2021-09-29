using CarShop.Controllers.BaseController;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;

namespace CarShop.Controllers.CarControllers
{
    public class ManufacturerController : BaseController<Manufacturer>
    {
        public ManufacturerController(IBaseRepository<Manufacturer> repository) : base(repository){}
    }
}