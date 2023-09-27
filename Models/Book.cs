namespace Booker.Models
{
    public class Book
    {
        public string? Id { get; }
        public string? AuthorId { get; }
        public string? PublisherId { get; }
        public string? Genreid { get; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
    }
}