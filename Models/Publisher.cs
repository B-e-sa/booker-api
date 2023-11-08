using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Publisher
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(30)]
        public string? Name { get; set; }
        
        public ICollection<Book>? Books { get; set; }
    }
}
