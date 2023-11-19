using System.ComponentModel.DataAnnotations;

namespace Booker.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        public List<Book> Books { get; } = new();

        public List<Genre> Genres { get; set; } = new();

        [Required]
        [StringLength(15)]
        public string? FirstName { get; set; }

        [StringLength(15)]
        public string? LastName { get; set; }

        public int? Note { get; set; }
    }
}
