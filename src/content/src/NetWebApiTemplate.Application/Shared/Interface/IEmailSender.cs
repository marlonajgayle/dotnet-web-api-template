using NetWebApiTemplate.Application.Features.EmailNotification;

namespace NetWebApiTemplate.Application.Shared.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message, EmailTemplates template);
    }
}