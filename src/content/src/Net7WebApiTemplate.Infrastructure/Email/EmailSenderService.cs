using FluentEmail.Core;
using Net7WebApiTemplate.Application.Features.EmailNotification;
using Net7WebApiTemplate.Application.Shared.Interface;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Net7WebApiTemplate.Infrastructure.Email
{
    public class EmailSenderService : IEmailSender
    {
        private const string TemplatePath = "Net7WebApiTemplate.Infrastructure.Email.Templates.{0}.cshtml";
        private readonly IFluentEmail _fluentEmail;
        private readonly EmailSenderOptions _senderOptions;

        public EmailSenderService(IFluentEmail fluentEmail, EmailSenderOptions senderOptions)
        {
            _fluentEmail = fluentEmail;
            _senderOptions = senderOptions;
        }

        public async Task SendEmailAsync(EmailMessage message, EmailTemplates template)
        {
            await _fluentEmail
                .SetFrom(_senderOptions.FromEmail)
                .To(message.To)
                .Subject(message.Subject)
                .UsingTemplateFromEmbedded(string.Format(TemplatePath, template), ToExpando(message.Model), GetType().Assembly)
                .SendAsync();
        }

        private static ExpandoObject ToExpando(object model)
        {
            if (model is ExpandoObject exp)
            {
                return exp;
            }

            IDictionary<string, object?> expando = new ExpandoObject();
            foreach (var propertyDescriptor in model.GetType().GetTypeInfo().GetProperties())
            {
                var obj = propertyDescriptor.GetValue(model);

                if (obj != null && IsAnonymousType(obj.GetType()))
                {
                    obj = ToExpando(obj);
                }

                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }

        private static bool IsAnonymousType(Type type)
        {
            bool hasCompilerGeneratedAttribute = type.GetTypeInfo()
                .GetCustomAttributes(typeof(CompilerGeneratedAttribute), false)
                .Any();

            bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            bool isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }
    }
}