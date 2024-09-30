using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Adapter.Driven.Database.Repository;
using Notification.Adapter.Driven.Database.UnitOfWork;
using Notification.Core.Domain.Adapters.Database.Repository;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;
using UnitOfWorkInstance = Notification.Adapter.Driven.Database.UnitOfWork.UnitOfWork;

namespace Notification.Adapter.Driven.Database;

public static class NotificationDatabaseDependency
{
    public static void AddNotificacaoDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUnitOfWork, UnitOfWorkInstance>();
        services.AddSingleton(sp =>
        {
            var connectionString = GetConnectionString(configuration);
            var context = new NotificacaoContext(connectionString);
            
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
            
            return context;
        });

        services.AddSingleton<INotificacaoRepository, NotificacaoRepository>();
        services.AddSingleton<INotificacaoAuditoriaRepository, NotificacaoAuditoriaRepository>();
    }

    private static string? GetConnectionString(IConfiguration configuration)
    {
        var connectionStringName = "DefaultConnection";
        #if !DEBUG
        connectionStringName = "DockerConnection";
        #endif

        return configuration.GetConnectionString(connectionStringName);
    }
}