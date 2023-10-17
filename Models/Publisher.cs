using System.ComponentModel.DataAnnotations;

namespace Booker.Models
{
    public class Publisher
    {
        public Guid? Id { get; set; }
        [StringLength(30)]
        public string? Name { get; set; }
    }
}