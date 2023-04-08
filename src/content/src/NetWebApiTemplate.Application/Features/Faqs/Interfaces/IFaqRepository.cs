using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Application.Features.Faqs.Interfaces
{
    public interface IFaqRepository
    {
        Task Add(Faq faq);
        Task<Faq> GetFaqByIdAsync(int id);
        Task<IEnumerable<Faq>> GetAll(int offset, int limit);
        Task Remove(int id);
    }
}