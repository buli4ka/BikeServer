using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShop.Models;
using BikeShop.Repositories.Interfaces;

using BikeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BikeController : ControllerBase
    {
        private IBaseRepository<Bike> BikeRepository { get; set; }

        private IBaseRepository<Manufacturer> ManufacturerRepository { get; set; }

        public BikeController(IBaseRepository<Bike> bikes
            , IBaseRepository<Manufacturer> manufacturer)
        {
            BikeRepository = bikes;
            ManufacturerRepository = manufacturer;
        }

        [HttpGet("getBikes")]
        public ActionResult<List<Bike>> GetAll()
        {
            return BikeRepository.GetAll();
        }
        
        // [HttpPost("getBikesByIds")]
        // public ActionResult<List<Bike>> GetAllByIds(List<Guid> ids)
        // {
        //     List<Bike> bikes = new List<Bike>();
        //     BikeRepository.GetAll().ForEach(i =>
        //     {
        //         if (ids.Contains(i.Id))
        //         {
        //             bikes.Add(i);
        //         }
        //     });
        //     return bikes ;
        // }
        

        [HttpGet("getBikeById/{id}")]
        public ActionResult<Bike> GetById(Guid id)
        {
            Bike bike = BikeRepository.GetById(id);
            if (bike is null)
                return NotFound();

            return bike;
        }
        [HttpPost("addBike")]
        public IActionResult Create(Bike bike)
        {
            try
            {
                BikeRepository.Create(bike);
                return CreatedAtAction(nameof(Create), new { id = bike.Id }, bike);
            }
            catch(Exception e) { return BadRequest(e); }
        }
        [HttpPut("updateBike/{id}")]
        public IActionResult Update(Guid id, Bike bike)
        {
            if (id != bike.Id)
                return BadRequest();

            Bike existingBike = BikeRepository.GetById(id);
            if (existingBike is null)
                return NotFound();
          
            BikeRepository.Update(existingBike);

            return NoContent();
        }

        [HttpDelete("deleteBike/{id}")]
        public IActionResult Delete(Guid id)
        {

            Bike bike = BikeRepository.GetById(id);

            if (bike is null)
            {
                return NotFound();

            }
            BikeRepository.Delete(id);


            return Ok();
        }
    }
}
