using System.ComponentModel.DataAnnotations;

namespace Booker.Models
{
    public class Genre
    {
        public Guid? Id { get; set; }
        [StringLength(30)]
        public string? Name { get; set; }
    }
}