using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        public Guid? Id { get; set; }

        public List<Genre> Genres { get; } = new();
        
        public List<Author> Authors { get; } = new();
 
        [StringLength(30)]
        public string? Name { get; set; }
    }
}
