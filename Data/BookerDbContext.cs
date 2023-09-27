using Booker.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Data
{
    public class BookerDbContext : DbContext
    {
        public BookerDbContext(DbContextOptions<BookerDbContext> options)
            : base(options)
        { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}