namespace Booker.Models
{
    public class Book
    {
        public Guid? Id { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? PublisherId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        public Author? Author { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<Genre>? Genres { get; set; }
    }
}