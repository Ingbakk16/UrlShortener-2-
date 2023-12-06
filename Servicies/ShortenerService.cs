using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using UrlShortener_2_.Data;
using UrlShortener_2_.Helpers;
using UrlShortener_2_.Entities;
using UrlShortener_2_.Data.Interfaces;
using Microsoft.AspNetCore.Http;

namespace UrlShortener_2_.Servicies
{
    public class ShortenerService
    {
        private readonly ShortenerDbContext _context;
        private readonly CreateShortUrl _shortUrlHelper;
        private readonly IUserService _userService;




        public ShortenerService(ShortenerDbContext context, CreateShortUrl shortUrlHelper, IUserService userService)
        {
            _context = context;
            _shortUrlHelper = shortUrlHelper;
            _userService = userService;

        }

        public async Task<string> ShortenUrl(string originalUrl, Guid userId, int categoryId)
        {
            var user = await _userService.GetUserById(userId);
            if (user.RemainingShortUrls > 0)
            {
                var existingUrlMapping = await _context.NewUrls
                    .FirstOrDefaultAsync(mapping => mapping.OriginalUrl == originalUrl);

                if (existingUrlMapping != null)
                {
                    // If it exists, return the existing short code
                    return existingUrlMapping.ShortUrl;
                }

                // If it doesn't exist, generate a new short code
                string shortCode = _shortUrlHelper.GenerateShortKey();

                // Create a new URL mapping and save it to the database
                var newUrlMapping = new NewUrl
                {
                    OriginalUrl = originalUrl,
                    ShortUrl = shortCode,
                    CategoryId = categoryId
                };

                _context.NewUrls.Add(newUrlMapping);

                // Decrement RemainingShortUrls for the user
                user.RemainingShortUrls -= 1;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the new short code
                return shortCode;
            }

            // If the user has no remaining short URL attempts, return null or handle it accordingly
            return null;
        }


        public async Task<string> RedirectUrl(string shortCode)
        {
            // Look up the original URL based on the short code
            var urlMapping = await _context.NewUrls
                .FirstOrDefaultAsync(mapping => mapping.ShortUrl == shortCode);

            if (urlMapping != null)
            {
                // If the short code is found, return the original URL
                return urlMapping.OriginalUrl;
            }

            // If the short code is not found, return null
            return null;

            
        }

        public async Task IncrementVisitCount(string shortCode)
        {
            // Retrieve the URL by the shortCode
            var url = await _context.NewUrls.SingleOrDefaultAsync(u => u.ShortUrl == shortCode);

            if (url != null)
            {
                url.VisitCounter++;
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }

     
    }
}

