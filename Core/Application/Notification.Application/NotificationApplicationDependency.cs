using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Service;
using Notification.Core.Domain.Services;

namespace Notification.Application;

public static class NotificationApplicationDependency
{
    public static void AddNotificationDependencyModule(this IServiceCollection services)
    {
        services.AddSingleton<INotificacaoWorkerService, NotificacaoWorkerService>();
    }
}