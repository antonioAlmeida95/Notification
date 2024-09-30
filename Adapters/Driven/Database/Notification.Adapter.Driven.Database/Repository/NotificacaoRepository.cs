using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Notification.Adapter.Driven.Database.UnitOfWork;
using Notification.Core.Domain.Adapters.Database.Repository;
using Notification.Core.Domain.Entities;

namespace Notification.Adapter.Driven.Database.Repository;

public class NotificacaoRepository (NotificacaoContext context) : IDisposable,
    IAsyncDisposable, INotificacaoRepository
{
    public async Task<bool> InserirNotificacaoAsync(Notificacao notificacao)
    {
        await context.AddAsync(notificacao);
        return await Commit();
    }

    public async Task<Notificacao?> ObterNotificacaoPorFiltrosAsync(Expression<Func<Notificacao, bool>> predicate,
        bool track = false) => await Query(predicate, track: track).FirstAsync();
    
    public void Dispose() => context.Dispose();

    public async ValueTask DisposeAsync() => await context.DisposeAsync();
    
    private async Task<bool> Commit()
    {
        try
        {
            var linhasAfetadas = await context.SaveChangesAsync();
            return linhasAfetadas > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Falha: {e.Message}");
            return false;
        }
    }
    
    private IQueryable<TX> Query<TX>(Expression<Func<TX, bool>>? expression = null, 
        Func<IQueryable<TX>, IIncludableQueryable<TX, object>>? include = null, bool track = false,
        int? skip = null, int? take = null) where TX : class
    {
        var query = context.GetQuery<TX>(track);

        if (expression != null)
            query = query.Where(expression);
        
        if (include != null)
            query = include(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return query;
    }
}