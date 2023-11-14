using Microsoft.AspNetCore.Mvc;
using UrlShortener_2_.Models.Dtos;
using UrlShortener_2_.Data.Interfaces;
using UrlShortener_2_.Entities;

namespace UrlShortener_2_.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            if (await _userService.IsUsernameTaken(registrationDto.Username))
            {
                return BadRequest("Username is already taken.");
            }

            if (await _userService.IsEmailTaken(registrationDto.Email))
            {
                return BadRequest("Email is already taken.");
            }

            // Hash and salt the password
            string hashedPassword = await _userService.HashPassword(registrationDto.Password);

            // Create a new user entity
            var newUser = new User
            {
                UserName = registrationDto.Username,
                Email = registrationDto.Email,
                PasswordHash = hashedPassword
                // You can add more user properties here
            };

            // Save the user to the database
            await _userService.CreateUser(newUser);

            // Return a success response
            return Ok("Registration successful.");
        }
    }
    }
