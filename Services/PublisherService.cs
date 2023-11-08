using Booker.Models;
using Booker.Repositories.Models;

namespace Booker.Services
{
    public class PublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository PublisherRepository) => _publisherRepository = PublisherRepository;

        public async Task<Publisher?> FindById(Guid id) => await _publisherRepository.FindById(id);

        public async Task<Publisher> Add(Publisher Publisher) => await _publisherRepository.Add(Publisher);

        public async Task<List<Publisher>> FindAll(int limit, int offset) =>
            await _publisherRepository.FindAll(limit, offset);

        public async Task<Publisher> Update(Publisher Publisher) => await _publisherRepository.Update(Publisher);

        public async Task<Publisher> Delete(Publisher Publisher) => await _publisherRepository.Delete(Publisher);
    }
}