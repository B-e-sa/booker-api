using Booker.Data;
using Booker.Models;
using Booker.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Repositories.Implementations
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookerDbContext _dbContext;

        // TODO: APPLY SHORTHAND
        public PublisherRepository(BookerDbContext dbContext) => _dbContext = dbContext;

        public async Task<Publisher?> FindById(Guid id) => await _dbContext.Publishers.FindAsync(id);

        public async Task<List<Publisher>> FindAll(int limit, int offset)
        {
            return await _dbContext.Publishers
                .Select(Publisher => Publisher)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Publisher> Add(Publisher Publisher)
        {
            await _dbContext.Publishers.AddAsync(Publisher);

            await _dbContext.SaveChangesAsync();

            return Publisher;
        }

        public async Task<Publisher> Update(Publisher PublisherToUpdate)
        {
            _dbContext.Publishers.Update(PublisherToUpdate);

            await _dbContext.SaveChangesAsync();

            return PublisherToUpdate;
        }

        public async Task<Publisher> Delete(Publisher PublisherToDelete)
        {
            _dbContext.Publishers.Remove(PublisherToDelete);

            await _dbContext.SaveChangesAsync();

            return PublisherToDelete;
        }
    }
}