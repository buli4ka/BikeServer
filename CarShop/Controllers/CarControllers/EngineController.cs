using CarShop.Controllers.BaseController;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;

namespace CarShop.Controllers.CarControllers
{
    public class EngineController: BaseController<Engine>
    {
        public EngineController(IBaseRepository<Engine> repository) : base(repository){}
    }
}