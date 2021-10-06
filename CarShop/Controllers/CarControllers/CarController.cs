using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Controllers.BaseController;
using CarShop.Models;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Implementation;
using CarShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers.CarControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet("getAll")]
        public virtual ActionResult<List<Car>> GetAll()
        {
            return _carRepository.GetAll();
        }

        [HttpGet("getById/{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var requestCar = await _carRepository.GetById(id);
            if (requestCar is null)
                return NotFound();

            return Ok(new
            {
                requestCar.Id,
                requestCar.Name,
                requestCar.Price,
                requestCar.Description,
                Manufacturer = requestCar.Manufacturer.Name,
                Engine = requestCar.Engine.Name,
                Transmission = requestCar.Transmission.Name,
                WheelDrive = requestCar.WheelDrive.Name,
                Comforts = requestCar.Comfort.Select(c => c.Name),
                Securities = requestCar.Securities.Select(c => c.Name),
                Images = requestCar.Images.Select(image => image.Id),
            });
        }

        [HttpPost("add")]
        public IActionResult Create(Car requestCar)
        {
            var car = _carRepository.Create(requestCar);
            return CreatedAtAction(nameof(Create), new {id = car.Id}, car);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Car tModel)
        {
            if (id != tModel.Id)
                return BadRequest();

            var existing = await _carRepository.GetById(id);
            if (existing is null)
                return NotFound();

            _carRepository.Update(tModel);

            return NoContent();
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tModel = await _carRepository.GetById(id);

            if (tModel is null)
            {
                return NotFound();
            }

            _carRepository.Delete(id);

            return NoContent();
        }
    }
}