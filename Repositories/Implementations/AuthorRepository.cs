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

        public async Task<Author?> Delete(Guid id)
        {
            Author? authorToDelete = await FindById(id);

            if (authorToDelete is not null)
                _dbContext.Authors.Remove(authorToDelete);

            return authorToDelete;
        }

        public async Task<List<Author>> FindAll(int limit, int offset)
        {
            return await _dbContext.Authors
                 .Select(author => author)
                 .Skip(offset)
                 .Take(limit)
                 .ToListAsync();
        }

        public Task<Author?> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Author?> Update(Guid id)
        {
            Author? authorToUpdate = await FindById(id);

            if (authorToUpdate is not null)
                _dbContext.Authors.Update(authorToUpdate);

            return authorToUpdate;
        }
    }
}
