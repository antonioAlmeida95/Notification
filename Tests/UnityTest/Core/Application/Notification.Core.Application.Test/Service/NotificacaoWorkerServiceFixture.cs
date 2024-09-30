using System.Linq.Expressions;
using Moq;
using Moq.AutoMock;
using Notification.Application.Service;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;
using Notification.Core.Domain.Adapters.Integrations.Email;
using Notification.Core.Domain.Entities;

namespace Notification.Core.Application.Test.Service;

[CollectionDefinition(nameof(NotificacaoWorkerServiceCollection))]
public class NotificacaoWorkerServiceCollection : ICollectionFixture<NotificacaoWorkerServiceFixture> { }
public class NotificacaoWorkerServiceFixture
{
    public AutoMocker Mocker;
    private NotificacaoWorkerService _notificacaoWorkerService;

    public NotificacaoWorkerService ObterNotificacaoWorkerService()
    {
        Mocker = new AutoMocker();

        _notificacaoWorkerService = Mocker.CreateInstance<NotificacaoWorkerService>();

        return _notificacaoWorkerService;
    }

    public void SetupObterNotificacaoPorFiltrosAsync(IEnumerable<Notificacao> notificacoes)
    {
        Mocker.GetMock<IUnitOfWork>()
            .Setup(s => s.Notificacao.ObterNotificacaoPorFiltrosAsync(It.IsAny<Expression<Func<Notificacao, bool>>>(),
                It.IsAny<bool>())).ReturnsAsync((Expression<Func<Notificacao, bool>> predicate, bool _) =>
                notificacoes.FirstOrDefault(predicate.Compile()));
    }

    public void SetupSendEmail(bool sucesso = true)
    {
        Mocker.GetMock<INotificationEmail>()
            .Setup(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(sucesso);
    }

    public void SetupInserirNotificacaoAuditoriaAsync(bool sucesso = true)
    {
        Mocker.GetMock<IUnitOfWork>()
            .Setup(s => s.NotificacaoAuditoria.InserirNotificacaoAuditoriaAsync(It.IsAny<NotificacaoAuditoria>()))
            .ReturnsAsync(sucesso);
    }
}