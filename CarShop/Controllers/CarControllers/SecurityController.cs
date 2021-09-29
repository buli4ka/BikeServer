using CarShop.Models.CarAttributes;
using CarShop.Controllers.BaseController;
using CarShop.Repositories.Interfaces;

namespace CarShop.Controllers.CarControllers
{
    public class SecurityController : BaseController<Security>
    {
        public SecurityController(IBaseRepository<Security> repository) : base(repository)
        {
        }
    }
}