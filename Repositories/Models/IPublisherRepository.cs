using Booker.Models;

namespace Booker.Repositories.Models
{
    public interface IPublisherRepository
    {
        Task<Publisher> Add(Publisher Publisher);
        Task<Publisher?> FindById(Guid id);
        Task<List<Publisher>> FindAll(int limit, int offset);
        Task<Publisher> Update(Publisher Publisher);
        Task<Publisher> Delete(Publisher Publisher);
    }
}