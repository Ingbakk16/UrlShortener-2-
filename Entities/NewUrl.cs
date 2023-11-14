using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener_2_.Entities
{
    public class NewUrl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string OriginalUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(6)]
        public string ShortUrl { get; set; } = string.Empty;
        
        public int VisitCounter { get; set; } = 0;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
