using UrlShortener_2_.Entities;

namespace UrlShortener_2_.Data.Interfaces
{
    public interface ICategoryService
    {
        int GetOrCreateCategory(string categoryName);

        Task<List<NewUrl>> GetUrlsByCategory(string categoryName);
    }
}
