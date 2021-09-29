using System;
using System.Collections.Generic;
using System.Linq;
using CarShop.Controllers.BaseController;
using CarShop.Models;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers.CarControllers
{
    public class CarController : BaseController<Car>
    {
        private readonly IBaseRepository<Manufacturer> _manufacturerRepository ;
        private readonly IBaseRepository<Engine> _engineRepository ;
        private readonly IBaseRepository<WheelDrive> _wheelDriveRepository;
        private readonly IBaseRepository<Transmission> _transmissionRepository ;
        private readonly IBaseRepository<Comfort> _comfortRepository ;
        private readonly IBaseRepository<Security> _securityRepository ;


        public CarController(IBaseRepository<Car> repository
            , IBaseRepository<Manufacturer> manufacturerRepository
            , IBaseRepository<Engine> engineRepository
            , IBaseRepository<WheelDrive> wheelDriveRepository
            , IBaseRepository<Transmission> transmissionRepository
            , IBaseRepository<Comfort> comfortRepository
            , IBaseRepository<Security> securityRepository
        ) :
            base(repository)
        {
            _manufacturerRepository = manufacturerRepository;
            _engineRepository = engineRepository;
            _wheelDriveRepository = wheelDriveRepository;
            _transmissionRepository = transmissionRepository;
            _comfortRepository = comfortRepository;
            _securityRepository = securityRepository;
        }

        [HttpPost("add")]
        public override IActionResult Create(Car requestCar)
        {
            
            var car = new Car
            {
                Name = requestCar.Name,
                Description = requestCar.Description,
                Manufacturer = _manufacturerRepository.GetById(requestCar.Manufacturer.Id),
                Engine = _engineRepository.GetById(requestCar.Engine.Id),
                WheelDrive = _wheelDriveRepository.GetById(requestCar.WheelDrive.Id),
                Transmission = _transmissionRepository.GetById(requestCar.Transmission.Id),
              
                // Engine = {Id = requestCar.Engine.Id},
                // WheelDrive = {Id = requestCar.WheelDrive.Id},
                // Transmission = {Id = requestCar.Transmission.Id},
                // Manufacturer = {Id = requestCar.Manufacturer.Id},
                Comfort = requestCar.Comfort.Select(i => _comfortRepository.GetById(i.Id)).ToList(),
                Securities = requestCar.Securities.Select(i => _securityRepository.GetById(i.Id)).ToList(),
            };

            Repository.Create(car);
            return CreatedAtAction(nameof(Create), new {id = car.Id}, car);
        }
    }
}