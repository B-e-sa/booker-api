using Booker.Models;

namespace Booker.Services.Models
{
    public interface IBookService
    {
        Task<Book> Add(Book book);
        Task<Book?> FindById(string id);
        Task<List<Book>> FindAll();
        Task<Book> Update(string id);
        Task<Book> Delete(string id);
    }
}