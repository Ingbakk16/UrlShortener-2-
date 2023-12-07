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
        public string? OriginalUrl { get; set; } 

        [Required]
        [MaxLength(6)]
        public string? ShortUrl { get; set; } 
        
        public int VisitCounter { get; set; } = 0;

        [ForeignKey("Category")] // Match the name of the property in the referenced table (Category)

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("User")]

        public int UserId { get; set; }

        public User? User { get; set; }

    }
}
