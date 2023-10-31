using Booker.Models;
using Booker.Repositories.Models;
using Booker.Services.Models;

namespace Booker.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository bookRepository) => _authorRepository = bookRepository;

        public async Task<Author?> FindById(Guid id) => await _authorRepository.FindById(id);

        public async Task<Author> Add(Author author) => await _authorRepository.Add(author);

        public async Task<List<Author>> FindAll(int limit, int offset) =>
            await _authorRepository.FindAll(limit, offset);

        public async Task<Author?> Update(Guid id) => await _authorRepository.Update(id);

        public async Task<Author?> Delete(Guid id) => await _authorRepository.Delete(id);
    }
}
