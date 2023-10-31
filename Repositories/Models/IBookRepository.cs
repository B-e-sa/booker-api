using Booker.Models;

namespace Booker.Repositories.Models
{
    public interface IBookRepository
    {
        Task<Book> Add(Book book);
        Task<Book?> FindById(Guid id);
        Task<List<Book>> FindAll();
        Task<Book> Update(Guid id);
        Task<Book> Delete(Guid id);
    }
}