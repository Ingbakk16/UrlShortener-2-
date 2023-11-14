using System.ComponentModel.DataAnnotations;

namespace UrlShortener_2_.Models.Dtos
{
    public class UserRegistrationDto
    {
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
