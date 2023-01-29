using Microsoft.EntityFrameworkCore;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface INet7WebApiTemplateDbContext
    {
        DbSet<Faq> Faqs { get; set; }

        
    }
}