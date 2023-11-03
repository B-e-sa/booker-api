using Booker.Data;
using Booker.Models;
using Booker.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Repositories.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookerDbContext _dbContext;

        // TODO: APPLY SHORTHAND
        public GenreRepository(BookerDbContext dbContext) => _dbContext = dbContext;

        public async Task<Genre?> FindById(Guid id) => await _dbContext.Genres.FindAsync(id);

        public async Task<List<Genre>> FindAll(int limit, int offset)
        {
            return await _dbContext.Genres
                .Select(genre => genre)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _dbContext.Genres.AddAsync(genre);

            await _dbContext.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre> Update(Genre genreToUpdate)
        {
            _dbContext.Genres.Update(genreToUpdate);

            await _dbContext.SaveChangesAsync();

            return genreToUpdate;
        }

        public async Task<Genre> Delete(Genre genreToDelete)
        {
            _dbContext.Genres.Remove(genreToDelete);

            await _dbContext.SaveChangesAsync();

            return genreToDelete;
        }
    }
}
