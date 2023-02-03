using Net7WebApiTemplate.Application.Features.EmailNotification;

namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message, EmailTemplates template);
    }
}