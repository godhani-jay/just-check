﻿using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.Repositories;

namespace NZWalks_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        //godhani jay pravinbhai
        // patel alay tarunbhai
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            // Check if user is authenticated
            // Check username and password
            var user = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                // Generate a JWT Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is incorrect.");
        }
    }
}
