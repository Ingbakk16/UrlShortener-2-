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

            


            // Create a new user entity
            var newUser = new User
            {
                UserName = registrationDto.Username,
                Email = registrationDto.Email,
                // Store the password as-is (not recommended for security reasons)
                PasswordHash = registrationDto.Password,
                
                // You can add more user properties here
            };

            // Save the user to the database
            await _userService.CreateUser(newUser);

            // Return a success response
            return Ok("Registration successful.");
        }

        [HttpGet("{userId}/remaining-shortens")]
        public async Task<IActionResult> GetRemainingShortens(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return Ok(user.RemainingShortUrls);
        }

        [HttpPut("{userId}/reset-url-shorts")]
        public async Task<IActionResult> ResetUrlShorts(int userId)
        {
            await _userService.ResetUrlShorts(userId);
            return Ok("URL shorts reset successfully.");
        }
    }
    }
