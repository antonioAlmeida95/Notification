using Notification.Core.Domain.Model;

namespace Notification.Core.Domain.Services;

public interface INotificacaoWorkerService
{
    Task ProcessarMensagemAsync(Message? message);

}