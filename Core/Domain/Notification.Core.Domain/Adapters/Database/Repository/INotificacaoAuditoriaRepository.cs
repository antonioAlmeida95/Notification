using Notification.Core.Domain.Entities;

namespace Notification.Core.Domain.Adapters.Database.Repository;

public interface INotificacaoAuditoriaRepository
{
    Task<bool> InserirNotificacaoAuditoriaAsync(NotificacaoAuditoria notificacaoAuditoria);
}