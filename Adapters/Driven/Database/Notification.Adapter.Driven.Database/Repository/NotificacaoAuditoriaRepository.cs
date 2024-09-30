using Notification.Adapter.Driven.Database.UnitOfWork;
using Notification.Core.Domain.Adapters.Database.Repository;
using Notification.Core.Domain.Entities;

namespace Notification.Adapter.Driven.Database.Repository;

public class NotificacaoAuditoriaRepository(NotificacaoContext context) : IDisposable,
    IAsyncDisposable, INotificacaoAuditoriaRepository
{
    public async Task<bool> InserirNotificacaoAuditoriaAsync(NotificacaoAuditoria notificacaoAuditoria)
    {
        await context.AddAsync(notificacaoAuditoria);
        return await Commit();
    }

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
}