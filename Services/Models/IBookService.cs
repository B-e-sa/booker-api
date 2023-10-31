using Booker.Models;

namespace Booker.Services.Models
{
    public interface IBookService
    {
        Task<Book> Add(Book book);
        Task<Book?> FindById(Guid id);
        Task<List<Book>> FindAll();
        Task<Book> Update(Guid id);
        Task<Book> Delete(Guid id);
    }
}