using System;
using System.Collections.Generic;
using BikeShop.Models;
using BikeShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private IBaseRepository<Bike> BikeRepository { get; set; }
        private IBaseRepository<User> UserRepository { get; set; }
        private IBaseRepository<Cart> CartRepository { get; set; }

        public CartController(
            IBaseRepository<Bike> bikeRepository
            , IBaseRepository<User> userRepository
            , IBaseRepository<Cart> cartRepository)
        {
            BikeRepository = bikeRepository;
            UserRepository = userRepository;
            CartRepository = cartRepository;
        }


        [HttpGet("getCartItems/{userId}")]
        public ActionResult<List<Bike>> GetAll(Guid userId)
        {
            List<Bike> bikes = new List<Bike>();
            CartRepository.GetAll().ForEach(i =>
            {
                if (i.UserId == userId)
                {
                    bikes.Add(BikeRepository.GetById(i.BikeId));
                }
            });

            return bikes;
        }

        [HttpPost("addToCart")]
        public IActionResult AddToCart(Cart cart)
        {
            CartRepository.Create(cart);
            return Ok(cart);
        }
        //
        // [HttpDelete("deleteItemFromCart/{userId}/{bikeId}")]
        // public IActionResult DeleteItemFromCart(Guid userId, Guid bikeId)
        // {
        //     CartRepository.Delete()
        // }
    }
}