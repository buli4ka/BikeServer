using CarShop.Controllers.BaseController;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;

namespace CarShop.Controllers.CarControllers
{
    public class TransmissionController : BaseController<Transmission>
    {
        public TransmissionController(IBaseRepository<Transmission> repository) : base(repository){}
    }
}
