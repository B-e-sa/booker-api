using Booker.Models;

namespace Booker.Services.Models
{
    public interface IGenreService
    {
        Task<Genre> Add(Genre genre);
        Task<Genre?> FindById(Guid id);
        Task<List<Genre>> FindAll(int limit, int offset);
        Task<Genre> Update(Genre genre);
        Task<Genre> Delete(Genre genre);
    }
}
