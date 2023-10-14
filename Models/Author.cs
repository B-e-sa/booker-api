namespace Booker.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}