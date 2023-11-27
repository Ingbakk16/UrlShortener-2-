using Microsoft.AspNetCore.Mvc;
using UrlShortener_2_.Servicies;
using UrlShortener_2_.Data;
using UrlShortener_2_.Models.Dtos;
using UrlShortener_2_.Entities;
using System;

namespace UrlShortener_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly ShortenerService _ShortenerService;
        private readonly ShortenerDbContext _context;

        public ShortenerController(ShortenerService xyzService, ShortenerDbContext context)
        {
            _ShortenerService = xyzService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] NewUrlForCreationDto urlDto)
        {
            if (urlDto == null || string.IsNullOrEmpty(urlDto.OriginalUrl))
            {
                return BadRequest("Original URL cannot be empty.");
            }

            // Call the service to shorten the URL
            string shortUrl = await _ShortenerService.ShortenUrl(urlDto.OriginalUrl);

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
    }
}