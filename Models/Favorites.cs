using System.ComponentModel.DataAnnotations.Schema;

namespace Booker.Models
{
    public class Favorites
    {
        public Guid? Id { get; set; }

        [ForeignKey("Publisher")]
        public Guid? PublisherId { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }

        [ForeignKey("Author")]
        public Guid? AuthorId { get; set; }

        [ForeignKey("Book")]
        public Guid? BookId { get; set; }
    }
}