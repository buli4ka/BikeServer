using BikeShop.Models;
using BikeShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BikeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IBaseRepository<User> UserRepository { get; set; }

        public UserController(IBaseRepository<User> users)
        {
            UserRepository = users;
        }




        [HttpGet("getUsers")]
        public ActionResult<List<User>> GetAll()
        {
            return UserRepository.GetAll();
        }


        [HttpGet("getUserById/{id}")]
        public ActionResult<User> GetUser(Guid id)
        {
            User user = UserRepository.GetById(id);
            if (user is null)
                return NotFound();

            return user;
        }

        [HttpPost("registration")]
        public IActionResult Registration([Bind("Name,Surname,Email,Password")] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not valid input");

                if (UserRepository.GetAll().Find(i => i.Email == user.Email) != null)
                    return BadRequest("User with this Email is exists");
                if (user.Role is null)
                    user.Role = "user";


                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                UserRepository.Create(user);
                var tokenString = GenerateJSONWebToken(user);
                return CreatedAtAction(nameof(Registration), new { token = tokenString, userId = user.Id, userRole = user.Role });


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("login")]
        public IActionResult Login([Bind("Email,Password")] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                User potentialUser = UserRepository.GetAll().Find(i => i.Email == user.Email);
                if (potentialUser is null)
                    return BadRequest("User not found");

                if (!BCrypt.Net.BCrypt.Verify(user.Password, potentialUser.Password))
                    return BadRequest("Passwords do not match");

                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString, userId = potentialUser.Id, userRole = potentialUser.Role });


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = TokenConfig.TokenConfig.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(TokenConfig.TokenConfig.ISSUER,
              TokenConfig.TokenConfig.AUDIENCE,
              null,
              expires: DateTime.Now.AddMinutes(TokenConfig.TokenConfig.LIFETIME),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
