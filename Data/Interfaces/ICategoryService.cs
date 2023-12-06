using UrlShortener_2_.Entities;

namespace UrlShortener_2_.Data.Interfaces
{
    public interface ICategoryService
    {
        Category GetOrCreateCategory(string categoryName);

        Task<List<NewUrl>> GetUrlsByCategory(string categoryName);
    }
}
