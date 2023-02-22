using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Application.Features.Faqs.Interfaces
{
    public interface IFaqRepository
    {
        Task Add(Faq faq);
        Task<Faq> GetFaqByIdAsync(int id);
        Task<IEnumerable<Faq>> GetAll(int offset, int limit);
        Task Remove(int id);
    }
}