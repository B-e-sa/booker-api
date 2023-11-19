using System.ComponentModel.DataAnnotations.Schema;

namespace Booker.Models
{
    public class Review
    {
        public Guid? Id { get; set; }

        [ForeignKey("Book")]
        public Guid? BookId { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        
        public string? Text { get; set; }
    }
}