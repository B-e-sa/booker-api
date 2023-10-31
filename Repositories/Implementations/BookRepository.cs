using Booker.Data;
using Booker.Models;
using Booker.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookerDbContext _dbContext;

        // TODO: APPLY SHORTHAND
        public BookRepository(BookerDbContext dbContext) => _dbContext = dbContext;

        public async Task<Book?> FindById(Guid id) => await _dbContext.Books.FindAsync(id);

        public async Task<List<Book>> FindAll(int limit, int offset)
        {
            return await _dbContext.Books
                .Select(book => book)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Book> Add(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> Update(Guid id)
        {
            Book? bookToUpdate = await FindById(id);

            if (bookToUpdate is not null)
                _dbContext.Books.Update(bookToUpdate);

            return bookToUpdate;
        }

        public async Task<Book?> Delete(Guid id)
        {
            Book? bookToDelete = await FindById(id);

            if (bookToDelete is not null)
                _dbContext.Books.Remove(bookToDelete);

            return bookToDelete;
        }
    }
}
