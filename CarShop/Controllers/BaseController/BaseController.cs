using System;
using System.Collections.Generic;
using CarShop.Models.Base;
using CarShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers.BaseController
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TModel> : ControllerBase where TModel : BaseModel
    {
        protected IBaseRepository<TModel> Repository { get; }

        protected BaseController(IBaseRepository<TModel> repository)
        {
            Repository = repository;
        }

        [HttpGet("getAll")]
        public virtual ActionResult<List<TModel>> GetAll()
        {
            return Repository.GetAll();
        }


        [HttpGet("getById/{id:guid}")]
        public virtual ActionResult<TModel> Get(Guid id)
        {
            var tModel = Repository.GetById(id);
            if (tModel is null)
                return NotFound();

            return tModel;
        }
        
        [HttpPost("add")]
        public virtual IActionResult Create(TModel tModel)
        {
            Repository.Create(tModel);
            return CreatedAtAction(nameof(Create), new { id = tModel.Id }, tModel);
        }
        [HttpPut("update/{id:guid}")]
        public virtual IActionResult Update(Guid id, TModel tModel)
        {
            if (id != tModel.Id)
                return BadRequest();

            var existing = Repository.GetById(id);
            if (existing is null)
                return NotFound();

            Repository.Update(tModel);

            return NoContent();
        }

        [HttpDelete("delete/{id:guid}")]
        public virtual IActionResult Delete(Guid id)
        {
            var tModel = Repository.GetById(id);

            if (tModel is null)
            {
                return NotFound();
            }
            Repository.Delete(id);
            
            return NoContent();
        }
    }
}