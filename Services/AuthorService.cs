using Booker.Models;
using Booker.Repositories.Models;

namespace Booker.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository bookRepository) => _authorRepository = bookRepository;

        public async Task<Author?> FindById(Guid id) => await _authorRepository.FindById(id);

        public async Task<Author> Add(Author author) => await _authorRepository.Add(author);

        public async Task<List<Author>> FindAll(int limit, int offset) =>
            await _authorRepository.FindAll(limit, offset);

        public async Task<Author> Update(Author author) => await _authorRepository.Update(author);

        public async Task<Author> Delete(Author author) => await _authorRepository.Delete(author);
    }
}
