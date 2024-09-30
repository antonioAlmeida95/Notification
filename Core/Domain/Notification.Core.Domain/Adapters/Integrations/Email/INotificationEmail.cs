using Notification.Core.Domain.Model;

namespace Notification.Core.Domain.Adapters.Integrations.Email;

public interface INotificationEmail
{
    bool SendEmail(string email, string subject, string body);
}