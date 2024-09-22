namespace Notification.Core.Domain.Adapters.Integrations;

public interface INotificationEmail
{
    Task<bool> SendEmail();
}