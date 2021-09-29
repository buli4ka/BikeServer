using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Controllers.BaseController;
using CarShop.Models;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Interfaces;
using CarShop.ViewControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CarShop.Controllers.CarControllers
{
    public class ImageController : BaseController<Image>
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<Car> _carRepository;

        public ImageController(IBaseRepository<Image> repository, IBaseRepository<Car> carRepository,
            IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
            _carRepository = carRepository;
        }


        [HttpGet("getPreview/{carId:guid}")]
        public ActionResult GetFirst(Guid carId)
        {
            var image = Repository.GetAll().FirstOrDefault(i => i.CarId == carId);
            if (image is null)
                return NotFound();
            return new PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(), image.ImagePath),
                $"image/{image.ImageType}");
        }

        [HttpGet("getById/{imageId:guid}")]
        public override ActionResult<Image> Get(Guid imageId)
        {
            var image = Repository.GetById(imageId);
            if (image is null)
                return NotFound();
            return new PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(), image.ImagePath),
                $"image/{image.ImageType}");
        }
        
        [HttpGet("getIds/{carId:guid}")]
        public ActionResult<List<Guid>> GetIds(Guid carId)
        {
            var images = new List<Guid>();
            Repository.GetAll().ForEach(i => { if (i.CarId == carId) images.Add(i.Id); });
            return images;
        }

        [HttpPost("add/{carId:guid}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Add(Guid carId, [FromForm] FileView file)
        {
            try
            {
                var Image = file.File;
                var relativePath = _configuration["ImageFolder"] + '/' + carId.ToString() + '/';

                if (Image.Length <= 0) return Ok(file.Source);
                var imageType = Path.GetExtension(Image.FileName)[1..];
                var imageName = Guid.NewGuid().ToString();


                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
                relativePath += imageName + '.' + imageType;
                await using (var fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), relativePath),
                    FileMode.Create))
                {
                    await Image.CopyToAsync(fs);
                }

                file.Source = relativePath;
                file.Extension = Path.GetExtension(Image.FileName)[1..];
                var image = new Image
                {
                    ImageType = imageType,
                    ImageName = imageName,
                    ImagePath = relativePath,
                    Car = _carRepository.GetById(carId)
                };
                Repository.Create(image);
                return Ok(image);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
    }
}