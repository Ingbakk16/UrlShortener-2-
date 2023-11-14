using System.ComponentModel.DataAnnotations;

namespace UrlShortener_2_.Models.Dtos
{
    public class AuthenticationRequestDto
    {
        [Required]
        public string? userName { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
