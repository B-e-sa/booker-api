using Booker.Models;

namespace Booker.Services.Models
{
    public interface IAuthorService
    {
        Task<Author> Add(Author author);
        Task<Author?> FindById(Guid id);
        Task<List<Author>> FindAll(int limit, int offset);
        Task<Author> Update(Author author);
        Task<Author> Delete(Author author);
    }
}
