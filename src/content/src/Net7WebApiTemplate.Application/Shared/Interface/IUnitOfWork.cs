namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}