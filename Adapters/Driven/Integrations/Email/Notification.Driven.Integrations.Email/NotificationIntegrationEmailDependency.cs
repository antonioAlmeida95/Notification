using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Core.Domain.Adapters.Integrations.Email;
using Notification.Driven.Integrations.Email.Notification;
using Notification.Driven.Integrations.Email.Settings;

namespace Notification.Driven.Integrations.Email;

public static class NotificationIntegrationEmailDependency
{
    public static void AddNotificationEmailModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SmtpCredentials>(opt => configuration.GetSection("Credentials:Smtp").Bind(opt));
        services.AddSingleton<INotificationEmail, NotificationEmail>();
    }
}