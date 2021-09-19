using BikeShop.Models;
using BikeShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BikeShop.Controllers
{
    public class FileView
    {
        public IFormFile File { get; set; }
        public string source { get; set; }
        public long Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Extension { get; set; }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        private IBaseRepository<Image> ImageRepository { get; set; }
        private IBaseRepository<Bike> BikeRepository { get; set; }
        private readonly ILogger<ImageController> _logger;
        public ImageController(IBaseRepository<Image> imageRepository, IBaseRepository<Bike> bikeRepository, ILogger<ImageController> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            ImageRepository = imageRepository;
            BikeRepository = bikeRepository;
            _logger = logger;
        }

        [HttpGet("getFirstBikeImage/{id}")]
        public ActionResult GetFirst(Guid id)
        {

            Models.Image image = ImageRepository.GetAll().FirstOrDefault(i => i.BikeId == id);
            //if (image is null)
            //    return NotFound();

            return new PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(), image.ImagePath), $"image/{image.ImageType}");

        }
        [HttpGet("getIds/{bikeId}")]
        public ActionResult<List<Guid>> GetIds(Guid bikeId)
        {
            List<Guid> images = new List<Guid>();
            ImageRepository.GetAll().ForEach(i => { if (i.BikeId == bikeId) images.Add(i.Id); });
            return images;
        }
        [HttpGet("getBikeImage/{id}")]
        public ActionResult GetAll(Guid id)
        {

            Models.Image image = ImageRepository.GetById(id);
            
            return new PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(), image.ImagePath), $"image/{image.ImageType}");

            //var file = system.io.file.readallbytes("c:\\users\\aopalko\\desktop\\h.png");
            //return new FileContentResult(image.ImageData, $"image/{image.ImageType}");
        }

        [HttpPost("addImages/{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddImages(Guid id, [FromForm] FileView file)
        {
            try
            {
                var Image = file.File;
                string imageType;
                string imageName;
                string relativePath = Configuration["ImageFolder"] + '/' + id.ToString() + '/';

                if (Image.Length > 0)
                {
                    imageType = Path.GetExtension(Image.FileName).Substring(1);
                    imageName = Guid.NewGuid().ToString();


                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
                    relativePath += imageName + '.' + imageType;
                    using (var fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), relativePath), FileMode.Create))
                    {
                        await Image.CopyToAsync(fs);
                    }
                    file.source = relativePath;
                    file.Extension = Path.GetExtension(Image.FileName).Substring(1);
                    Image image = new Image();
                    image.ImageType = imageType;
                    image.ImageName = imageName;
                    image.ImagePath = relativePath;
                    image.BikeId = id;
                    ImageRepository.Create(image);
                }
                return Ok(file.source);



            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

    }
}
