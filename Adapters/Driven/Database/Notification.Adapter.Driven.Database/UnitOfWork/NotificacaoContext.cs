using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Notification.Adapter.Driven.Database.Mappings;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;
using Notification.Core.Domain.Entities;

namespace Notification.Adapter.Driven.Database.UnitOfWork;

public partial class NotificacaoContext: DbContext, INotificacaoContext
{
    public DbSet<Notificacao> Notificacao { get; set; }
    public DbSet<NotificacaoAuditoria> NotificacaoAuditoria { get; set; }
    
    private string? _connectionString;
    
    public NotificacaoContext(string? connectionString = null) => _connectionString = connectionString;

    public DatabaseFacade GetDatabase() => Database;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NotificacaoMapping());
        modelBuilder.ApplyConfiguration(new NotificacaoAuditoriaMapping());
    }
}