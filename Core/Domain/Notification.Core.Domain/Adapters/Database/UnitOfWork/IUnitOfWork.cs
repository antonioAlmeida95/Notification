using Notification.Core.Domain.Adapters.Database.Repository;

namespace Notification.Core.Domain.Adapters.Database.UnitOfWork;

public interface IUnitOfWork
{
    INotificacaoRepository Notificacao { get; }
    INotificacaoAuditoriaRepository NotificacaoAuditoria { get; }
}