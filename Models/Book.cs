using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    public class Book
    {
        public Guid? Id { get; set; }

        public Guid? AuthorId { get; set; }

        public Guid? PublisherId { get; set; }

        [StringLength(30)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [StringLength(13)]
        public string? ISBN { get; set; }

        public Author? Author { get; set; }

        public Publisher? Publisher { get; set; }

        public ICollection<Genre>? Genres { get; set; }
    }
}