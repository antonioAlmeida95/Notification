using System.Text;
using Microsoft.Extensions.Logging;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;
using Notification.Core.Domain.Adapters.Integrations.Email;
using Notification.Core.Domain.Entities;
using Notification.Core.Domain.Model;
using Notification.Core.Domain.Services;

namespace Notification.Application.Service;

public class NotificacaoWorkerService : INotificacaoWorkerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationEmail _notificationHandler;
    private readonly ILogger<NotificacaoWorkerService> _logger;

    public NotificacaoWorkerService(IUnitOfWork unitOfWork, INotificationEmail notificationHandler,
        ILogger<NotificacaoWorkerService> logger)
    {
        _unitOfWork = unitOfWork;
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    public async Task ProcessarMensagemAsync(Message? message)
    {
        if(message is null) return;
        
        var template = await 
            _unitOfWork.Notificacao.ObterNotificacaoPorFiltrosAsync(m =>
                m.Id == message.NotificationId.GetValueOrDefault());

        if (template is null)
        {
            _logger.LogInformation("Template da Notificação {NotificationId} não encontrada", message.NotificationId);
            return;
        }

        var body = CriarBodyEmail(message.Nome ?? string.Empty, template.Corpo ?? string.Empty);
        var emailSend = _notificationHandler.SendEmail(message.Email ?? string.Empty, template.Titulo ?? string.Empty,
            body);

        if (emailSend)
            await RegistrarEnvioNotificacao(message);
    }

    private async Task RegistrarEnvioNotificacao(Message message)
    {
        var audit = new NotificacaoAuditoria(destinatario: message.Email, origem: message.Origem,
            dataEnvio: DateTimeOffset.UtcNow, notificacaoId: message.NotificationId.GetValueOrDefault());
        
        await _unitOfWork.NotificacaoAuditoria.InserirNotificacaoAuditoriaAsync(audit);
    }

    private static string CriarBodyEmail(string nome, string corpo)
    {
        var body = new StringBuilder();
        body.Append("<h3 style=\"color: #2e6c80;\">");
        body.Append(nome);
        body.Append("</h3>");
        body.Append("<p><strong>");
        body.Append(corpo);
        body.Append("</strong></p>");
        
        return body.ToString();
    }
}