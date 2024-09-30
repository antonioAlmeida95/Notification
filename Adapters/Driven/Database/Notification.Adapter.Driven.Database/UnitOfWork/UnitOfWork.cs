using Microsoft.Extensions.DependencyInjection;
using Notification.Core.Domain.Adapters.Database.Repository;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;

namespace Notification.Adapter.Driven.Database.UnitOfWork;

public class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
{
    public INotificacaoRepository Notificacao => 
        serviceProvider.GetRequiredService<INotificacaoRepository>();

    public INotificacaoAuditoriaRepository NotificacaoAuditoria =>
        serviceProvider.GetRequiredService<INotificacaoAuditoriaRepository>();
}