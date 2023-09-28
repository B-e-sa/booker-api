using Booker.Models;
using Booker.Repositories.Implementations;
using Booker.Repositories.Models;
using Booker.Services.Models;

namespace Booker.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Book?> FindById(string id) => await _bookRepository.FindById(id);

        public async Task<Book> Add(Book book) => await _bookRepository.Add(book);

        public async Task<List<Book>> FindAll() => await _bookRepository.FindAll();

        public async Task<Book> Update(string id) => await _bookRepository.Update(id);

        public async Task<Book> Delete(string id) => await _bookRepository.Delete(id);
    }
}