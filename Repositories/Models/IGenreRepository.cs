using Booker.Models;

namespace Booker.Repositories.Models
{
    public interface IGenreRepository
    {
        Task<Genre> Add(Genre genre);
        Task<Genre?> FindById(Guid id);
        Task<List<Genre>> FindAll(int limit, int offset);
        Task<Genre> Update(Genre genre);
        Task<Genre> Delete(Genre genre);
    }
}
