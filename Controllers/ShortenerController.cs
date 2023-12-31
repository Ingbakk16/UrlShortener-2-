﻿using Microsoft.AspNetCore.Mvc;
using UrlShortener_2_.Servicies;
using UrlShortener_2_.Data;
using UrlShortener_2_.Models.Dtos;
using UrlShortener_2_.Entities;
using System;
using Microsoft.AspNetCore.Authorization;
using UrlShortener_2_.Data.Interfaces;
using System.Security.Claims;

namespace UrlShortener_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly ShortenerService _ShortenerService;
        private readonly ShortenerDbContext _context;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;




        public ShortenerController(ShortenerService xyzService, ShortenerDbContext context, IUserService userService, ICategoryService categoryService)
        {
            _ShortenerService = xyzService;
            _context = context;
            _userService = userService;
            _categoryService = categoryService;


        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] NewUrlForCreationDto urlDto)
        {
           
            // Obtén el identificador del usuario desde el token utilizando el servicio de usuario
            string userId = _userService.ObtainUserIdFromToken();

            if (urlDto == null || userId == null)
            {
                // Manejo de la situación donde urlDto o urlDto.UserId es null.
                return BadRequest("UserId cannot be null.");
            }

            // Convierte el userId a Int
         
            int userIdInt = int.Parse(userId);

            int category = _categoryService.GetOrCreateCategory(urlDto.CategoryName);
            if (category == null)
            {
                return BadRequest("Category could not be retrieved or created.");
            }


            // Llama al método ShortenUrl del servicio de acortamiento de URL
            string shortUrl = await _ShortenerService.ShortenUrl(urlDto.OriginalUrl, userIdInt, category);

            if (string.IsNullOrEmpty(shortUrl))
            {
                return BadRequest("URL could not be shortened.");
            }

            return Ok(shortUrl);
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectUrl(string shortCode)
        {
            string originalUrl = await _ShortenerService.RedirectUrl(shortCode);

            if (string.IsNullOrEmpty(originalUrl))
            {
                return NotFound("Short URL not found.");
            }
            await _ShortenerService.IncrementVisitCount(shortCode);

            // Redirect to the original URL
            return Redirect(originalUrl);
        }

        [HttpGet("urlsByCategory/")]
        public async Task<IActionResult> GetUrlsByCategory(string categoryName)
        {
            var urls = await _categoryService.GetUrlsByCategory(categoryName);

            if (urls == null || !urls.Any())
            {
                return NotFound("No URLs found for the specified category.");
            }

            return Ok(urls);
        }






    }
}