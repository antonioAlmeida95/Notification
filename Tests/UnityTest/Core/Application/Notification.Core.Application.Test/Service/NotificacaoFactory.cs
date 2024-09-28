using Notification.Core.Domain.Entities;
using Notification.Core.Domain.Model;

namespace Notification.Core.Application.Test.Service;

public static class NotificacaoFactory
{
    public static Message CriarMessage(string? email = null, string? nome = null, string? origem = null,
        Guid? notificationId = null) => new()
    {
        Email = email,
        Nome = nome,
        Origem = origem,
        NotificationId = notificationId
    };

    public static Notificacao CriarNotificacao(string? titulo = null, string? corpo = null,
            Guid? notificacaoId = null) => new(titulo, corpo, notificacaoId);
}