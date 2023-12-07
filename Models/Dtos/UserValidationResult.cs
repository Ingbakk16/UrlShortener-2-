using UrlShortener_2_.Entities;

namespace UrlShortener_2_.Models.Dtos
{
    public class UserValidationResult
    {
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
