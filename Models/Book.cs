using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class Book
    {
        public Guid? Id { get; set; }

        public Guid? AuthorId { get; set; }

        public Guid? PublisherId { get; set; }

        [Required]
        [StringLength(30)]
        public string? Title;

        public string? Description { get; set; }

        [Required]
        [RegularExpression(
            @"^\d{3}-\d-\d{2}-\d{6}-\d$",
            ErrorMessage = "Invalid ISBN format"
        )]
        public string? ISBN  { get; set; }

        public Author? Author { get; set; }

        public Publisher? Publisher { get; set; }

        public ICollection<Genre>? Genres { get; set; }
    }
}
