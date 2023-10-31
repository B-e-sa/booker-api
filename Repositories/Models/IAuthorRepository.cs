using Booker.Models;

namespace Booker.Repositories.Models
{
    public interface IAuthorRepository
    {
        Task<Author> Add(Author author);
        Task<Author?> FindById(Guid id);
        Task<List<Author>> FindAll(int limit, int offset);
        Task<Author?> Update(Guid id);
        Task<Author?> Delete(Guid id);
    }
}
