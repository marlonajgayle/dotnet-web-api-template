namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface IGitHubService
    {
        Task<string> LoadAccountAsync(string username);
    }
}