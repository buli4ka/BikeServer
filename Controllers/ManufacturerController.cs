using BikeShop.Models;
using BikeShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private IBaseRepository<Manufacturer> ManufacturerRepository { get; set; }
        public ManufacturerController(IBaseRepository<Manufacturer> manufacturerRepository)
        {
            ManufacturerRepository = manufacturerRepository;
        }


        [HttpGet("getManufacturers")]
        public ActionResult<List<Manufacturer>> GetAll()
        {
            return ManufacturerRepository.GetAll();
        }


        [HttpGet("getManufacturerById/{id}")]
        public ActionResult<Manufacturer> GetUser(Guid id)
        {
            Manufacturer manufacturer = ManufacturerRepository.GetById(id);
            if (manufacturer is null)
                return NotFound();

            return manufacturer;
        }
        [HttpPost("addManufacturer")]
        public IActionResult Create(Manufacturer manufacturer)
        {
            
            ManufacturerRepository.Create(manufacturer);
            return CreatedAtAction(nameof(Create), new { id = manufacturer.Id }, manufacturer);
        }
        [HttpPut("updateManufacturer/{id}")]
        public IActionResult Update(Guid id, Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
                return BadRequest();

            Manufacturer existingManufacturer = ManufacturerRepository.GetById(id);
            if (existingManufacturer is null)
                return NotFound();

            ManufacturerRepository.Update(manufacturer);

            return NoContent();
        }

        [HttpDelete("deleteManufacturer/{id}")]
        public IActionResult Delete(Guid id)
        {

            Manufacturer manufacturer = ManufacturerRepository.GetById(id);

            if (manufacturer is null)
            {
                return NotFound();

            }
            ManufacturerRepository.Delete(id);


            return NoContent();
        }

    }
}
