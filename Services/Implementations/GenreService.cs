using Booker.Models;
using Booker.Repositories.Models;
using Booker.Services.Models;

namespace Booker.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public async Task<Genre?> FindById(Guid id) => await _genreRepository.FindById(id);

        public async Task<Genre> Add(Genre genre) => await _genreRepository.Add(genre);

        public async Task<List<Genre>> FindAll(int limit, int offset) =>
            await _genreRepository.FindAll(limit, offset);

        public async Task<Genre> Update(Genre genre) => await _genreRepository.Update(genre);

        public async Task<Genre> Delete(Genre genre) => await _genreRepository.Delete(genre);
    }
}
