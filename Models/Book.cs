using System.ComponentModel.DataAnnotations;

namespace Booker.Models
{
    public class Book
    {
        public Guid? Id { get; set; }

        public Guid? AuthorId { get; set; }

        public Guid? PublisherId { get; set; }

        [Required]
        [StringLength(30)]
        public string Title = "Unknown";

        public string? Description { get; set; }

        [Required]
        [StringLength(13)]
        public string? ISBN { get; set; }

        public Author? Author { get; set; }

        public Publisher? Publisher { get; set; }

        public ICollection<Genre>? Genres { get; set; }
    }
}
