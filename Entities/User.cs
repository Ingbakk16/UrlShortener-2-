using System.ComponentModel.DataAnnotations;

namespace UrlShortener_2_.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(16)]
        public string? UserName { get; internal set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]

        public string PasswordHash { get; set; } // Al final no la almaceno hasheada
       
    }
}
