namespace NetWebApiTemplate.Application.Shared.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}