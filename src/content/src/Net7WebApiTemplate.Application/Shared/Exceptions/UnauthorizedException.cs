namespace Net7WebApiTemplate.Application.Shared.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
        : base(message)
        {
        }
    }
}