using Microsoft.EntityFrameworkCore;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace Notification.Adapter.Driven.Database.UnitOfWork;

public partial class NotificacaoContext
{
    /// <inheritdoc />
    public IQueryable<T> GetQuery<T>(bool track) where T : class => track ? Set<T>() : Set<T>().AsNoTracking();
    
    /// <inheritdoc />
    public IQueryable<T> GetQuery<T>() where T : class => Set<T>().AsNoTracking();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(optionsBuilder.IsConfigured) return;
        
        if (string.IsNullOrEmpty(_connectionString))
            _connectionString = LoadConnectionString();
        
        ArgumentException.ThrowIfNullOrWhiteSpace(_connectionString);

        optionsBuilder.UseNpgsql(_connectionString);
    }
    
    /// <summary>
    ///     Método para obtenção da string de conexão do arquivo de conexão
    /// </summary>
    /// <param name="connectionStringName">Identificador do campo</param>
    /// <returns>String de Conexão</returns>
    private static string LoadConnectionString(string connectionStringName = "DefaultConnection")
    {
        #if !DEBUG
        connectionStringName = "DockerConnection";
        #endif
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "//Config")
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var connStr = config.GetConnectionString(connectionStringName);
        return connStr ?? string.Empty;
    }
}