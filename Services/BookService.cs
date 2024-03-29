using Booker.Models;
using Booker.Repositories.Models;

namespace Booker.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Book?> FindById(Guid id) => await _bookRepository.FindById(id);

        public async Task<Book> Add(Book book) => await _bookRepository.Add(book);

        public async Task<List<Book>> FindAll(int limit, int offset) =>
            await _bookRepository.FindAll(limit, offset);

        public async Task<Book> Update(Book book) => await _bookRepository.Update(book);

        public async Task<Book> Delete(Book book) => await _bookRepository.Delete(book);
    }
}
