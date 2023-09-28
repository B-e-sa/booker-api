using Booker.Data;
using Booker.Models;
using Booker.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookerDbContext _dbContext;

        public BookRepository(BookerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> FindById(string id) => await _dbContext.Books.FindAsync(id);

        public async Task<List<Book>> FindAll() => await _dbContext.Books.ToListAsync();
        public async Task<Book> Add(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            return book;
        }

        public async Task<Book> Update(string id)
        {
            Book bookToUpdate = await this.FindById(id);

            _dbContext.Books.Update(bookToUpdate);

            return bookToUpdate;
        }

        public async Task<Book> Delete(string id)
        {
            Book bookToDelete = await this.FindById(id);

            _dbContext.Books.Remove(bookToDelete);

            return bookToDelete;
        }
    }
}