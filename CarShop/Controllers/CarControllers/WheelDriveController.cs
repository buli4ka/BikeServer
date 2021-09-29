using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;
using CarShop.Controllers.BaseController;

namespace CarShop.Controllers.CarControllers
{
    public class WheelDriveController: BaseController<WheelDrive>
    {
        public WheelDriveController(IBaseRepository<WheelDrive> repository) : base(repository){}
    }
  
}