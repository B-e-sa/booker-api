using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        public Guid? Id { get; set; }
        [StringLength(30)]
        public string? Name { get; set; }
    }
}
