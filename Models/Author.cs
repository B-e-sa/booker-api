using System.ComponentModel.DataAnnotations;

namespace Booker.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(30)]
        public string? FirstName { get; set; }
        [StringLength(30)]
        public string? LastName { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
