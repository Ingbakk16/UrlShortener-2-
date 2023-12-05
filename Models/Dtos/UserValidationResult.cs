using UrlShortener_2_.Entities;

namespace UrlShortener_2_.Models.Dtos
{
    public class UserValidationResult
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
