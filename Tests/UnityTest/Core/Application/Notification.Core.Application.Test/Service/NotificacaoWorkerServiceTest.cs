using System.Linq.Expressions;
using Moq;
using Notification.Application.Service;
using Notification.Core.Domain.Adapters.Database.UnitOfWork;
using Notification.Core.Domain.Adapters.Integrations.Email;
using Notification.Core.Domain.Entities;

namespace Notification.Core.Application.Test.Service;

[Collection(nameof(NotificacaoWorkerServiceCollection))]
public class NotificacaoWorkerServiceTest
{
    private readonly NotificacaoWorkerServiceFixture _serviceFixture;
    private readonly NotificacaoWorkerService _service;

    public NotificacaoWorkerServiceTest(NotificacaoWorkerServiceFixture fixture)
    {
        _serviceFixture = fixture;
        _service = _serviceFixture.ObterNotificacaoWorkerService();
    }
    
    [Fact]
    public async Task ProcessarMensagemAsync_EnviaMensagemSucesso()
    {
        //Arrange
        var notificacaoId = Guid.NewGuid();
        var message = NotificacaoFactory.CriarMessage(email: "teste@mail.com", "Teste", "XPTO", notificacaoId);
        var notificacao = NotificacaoFactory.CriarNotificacao("Bem vindo", "Olá bem vindo", notificacaoId);

        var notificacoes = new[]
        {
            NotificacaoFactory.CriarNotificacao(),
            notificacao,
            NotificacaoFactory.CriarNotificacao()
        };

        //Setup
        _serviceFixture.SetupObterNotificacaoPorFiltrosAsync(notificacoes);
        _serviceFixture.SetupSendEmail();
        _serviceFixture.SetupInserirNotificacaoAuditoriaAsync();
        
        //Act
        await _service.ProcessarMensagemAsync(message);

        //Assert
        _serviceFixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Notificacao.ObterNotificacaoPorFiltrosAsync(It.IsAny<Expression<Func<Notificacao, bool>>>(),
                    It.Is<bool>(t => !t)),
                Times.Once);

        _serviceFixture.Mocker.GetMock<INotificationEmail>()
            .Verify(s => s.SendEmail(It.Is<string>(n => n.Equals(message.Email)),
                    It.Is<string>(sb => sb.Equals(notificacao.Titulo)),
                    It.IsAny<string>()),
                Times.Once);

        _serviceFixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(
                s => s.NotificacaoAuditoria.InserirNotificacaoAuditoriaAsync(It.Is<NotificacaoAuditoria>(noa =>
                    noa.NotificacaoId == notificacaoId &&
                    noa.Destinatario != null &&
                    noa.Destinatario.Equals(message.Email) &&
                    noa.Origem != null &&
                    noa.Origem.Equals(message.Origem))),
                Times.Once);
    }
    
    [Fact]
    public async Task ProcessarMensagemAsync_TemplateNaoEncontradoFalha()
    {
        //Arrange
        var notificacaoId = Guid.NewGuid();
        var message = NotificacaoFactory.CriarMessage(email: "teste@mail.com", "Teste", "XPTO", notificacaoId);
       
        var notificacoes = new[]
        {
            NotificacaoFactory.CriarNotificacao(),
            NotificacaoFactory.CriarNotificacao()
        };

        //Setup
        _serviceFixture.SetupObterNotificacaoPorFiltrosAsync(notificacoes);
        
        //Act
        await _service.ProcessarMensagemAsync(message);

        //Assert
        _serviceFixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Notificacao.ObterNotificacaoPorFiltrosAsync(It.IsAny<Expression<Func<Notificacao, bool>>>(),
                    It.Is<bool>(t => !t)),
                Times.Once);

        _serviceFixture.Mocker.GetMock<INotificationEmail>()
            .Verify(s => s.SendEmail(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);

        _serviceFixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.NotificacaoAuditoria.InserirNotificacaoAuditoriaAsync(It.IsAny<NotificacaoAuditoria>()),
                Times.Never);
    }
    
    [Fact]
    public async Task ProcessarMensagemAsync_MensagemNulaFalha()
    {
        //Arrange
        //Setup
        
        //Act
        await _service.ProcessarMensagemAsync(null);

        //Assert
        _serviceFixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Notificacao.ObterNotificacaoPorFiltrosAsync(It.IsAny<Expression<Func<Notificacao, bool>>>(),
                    It.IsAny<bool>()),
                Times.Never);

        _serviceFixture.Mocker.GetMock<INotificationEmail>()
            .Verify(s => s.SendEmail(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);

        _serviceFixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.NotificacaoAuditoria.InserirNotificacaoAuditoriaAsync(It.IsAny<NotificacaoAuditoria>()),
                Times.Never);
    }
}