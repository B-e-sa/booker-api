using Booker.Data;
using Booker.Models;
using Booker.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookerDbContext _dbContext;

        public AuthorRepository(BookerDbContext dbContext) => _dbContext = dbContext;

        public async Task<Author> Add(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return author;
        }

        public async Task<Author> Delete(Author author)
        {
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();

            return author;
        }

        public async Task<List<Author>> FindAll(int limit, int offset)
        {
            return await _dbContext.Authors
                 .Select(author => author)
                 .Skip(offset)
                 .Take(limit)
                 .ToListAsync();
        }

        public async Task<Author?> FindById(Guid id) => await _dbContext.Authors.FindAsync(id);

        public async Task<Author> Update(Author author)
        {
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return author;
        }
    }
}
