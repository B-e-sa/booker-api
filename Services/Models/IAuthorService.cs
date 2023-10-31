using Booker.Models;

namespace Booker.Services.Models
{
    public interface IAuthorService
    {
        Task<Author> Add(Author book);
        Task<Author?> FindById(Guid id);
        Task<List<Author>> FindAll(int limit, int offset);
        Task<Author?> Update(Guid id);
        Task<Author?> Delete(Guid id);
    }
}
