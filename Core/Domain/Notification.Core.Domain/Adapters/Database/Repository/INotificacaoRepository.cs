using System.Linq.Expressions;
using Notification.Core.Domain.Entities;

namespace Notification.Core.Domain.Adapters.Database.Repository;

public interface INotificacaoRepository
{
    Task<bool> InserirNotificacaoAsync(Notificacao notificacao);

    Task<Notificacao?> ObterNotificacaoPorFiltrosAsync(Expression<Func<Notificacao, bool>> predicate,
        bool track = false);
}