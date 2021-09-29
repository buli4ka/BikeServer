using System;
using System.IdentityModel.Tokens.Jwt;
using CarShop.Controllers.BaseController;
using CarShop.Models;
using CarShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace CarShop.Controllers.UserControllers
{
    public class UserController : BaseController<User>
    {
        public UserController(IBaseRepository<User> repository) : base(repository)
        {
            
        }
        
        [HttpPost("registration")]
        public IActionResult Registration([Bind("Name,Surname,Email,Password")] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not valid input");

                if (Repository.GetAll().Find(i => i.Email == user.Email) != null)
                    return BadRequest("User with this Email is exists");

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                Repository.Create(user);
                var tokenString = GenerateJsonWebToken(user);
                return CreatedAtAction(nameof(Registration), new { token = tokenString, userId = user.Id });


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
                var potentialUser = Repository.GetAll().Find(i => i.Email == user.Email);
                if (potentialUser is null)
                    return BadRequest("User not found");

                if (!BCrypt.Net.BCrypt.Verify(user.Password, potentialUser.Password))
                    return BadRequest("Passwords do not match");

                var tokenString = GenerateJsonWebToken(user);
                return Ok(new { token = tokenString, userId = potentialUser.Id });


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        private static string GenerateJsonWebToken(User userInfo)
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