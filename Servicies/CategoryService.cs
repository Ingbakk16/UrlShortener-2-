using UrlShortener_2_.Data.Interfaces;
using UrlShortener_2_.Data;
using UrlShortener_2_.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace UrlShortener_2_.Servicies
{
    public class CategoryService : ICategoryService
    {
        private readonly ShortenerDbContext _context;

        public CategoryService(ShortenerDbContext context)
        {
            _context = context;
        }

        public int GetOrCreateCategory(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                // Si no se encuentra, crear una nueva categoría
                category = new Category
                {
                    Name = categoryName
                };

                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            return category.CategoryId;
        }

        public async Task<List<NewUrl>> GetUrlsByCategory(string categoryName)
        {
            return await _context.NewUrls
                .Where(url => url.Category.Name == categoryName)
                .ToListAsync();
        }
    }
}
