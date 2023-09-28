using Booker.Models;

namespace Booker.Repositories.Models
{
    public interface IBookRepository
    {
        Task<Book> Add(Book book);
        Task<Book?> FindById(string id);
        Task<List<Book>> FindAll();
        Task<Book> Update(string id);
        Task<Book> Delete(string id);
    }
}