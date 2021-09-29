using CarShop.Controllers.BaseController;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;

namespace CarShop.Controllers.CarControllers
{
    public class ComfortController : BaseController<Comfort>
    {
        public ComfortController(IBaseRepository<Comfort> repository):base(repository){}
    }
}