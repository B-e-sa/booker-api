using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class Book
    {
        public Guid? Id { get; set; }

        [ForeignKey("Publisher")]
        public Guid? PublisherId { get; set; }
        public Publisher Publiser { get; } = null!;

        [ForeignKey("Author")]
        public Guid? AuthorId { get; set; }
        public Author Author { get; } = null!;

        public List<Genre> Genres { get; set; } = new();

        [Required]
        [StringLength(30)]
        public string? Title;

        public string? Description { get; set; }

        [Required]
        [RegularExpression(
            @"^\d{3}-\d-\d{2}-\d{6}-\d$",
            ErrorMessage = "Invalid ISBN format (must be xxx-x-xx-xxxxxx-x)"
        )]
        public string? ISBN { get; set; }

        public int? Note { get; set; }
    }
}
