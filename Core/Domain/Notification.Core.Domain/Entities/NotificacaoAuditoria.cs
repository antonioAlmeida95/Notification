using FluentValidation;

namespace Notification.Core.Domain.Entities;

public class NotificacaoAuditoria : EntidadeBase<NotificacaoAuditoria>
{
    public string? Destinatario { get; }
    public string? Origem { get; }
    public DateTimeOffset DataEnvio { get; }
    public Guid NotificacaoId { get; }
    public virtual Notificacao Notificacao { get; }
    
    public NotificacaoAuditoria() { }

    public NotificacaoAuditoria(string? destinatario, string? origem,
        DateTimeOffset dataEnvio, Guid notificacaoId, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Destinatario = destinatario;
        Origem = origem;
        DataEnvio = dataEnvio;
        NotificacaoId = notificacaoId;
    }
    
    public NotificacaoAuditoria(string? destinatario, string? origem,
        DateTimeOffset dataEnvio, Guid notificacaoId) 
        : this(destinatario, origem, dataEnvio, notificacaoId, null) { }

    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Origem)
            .NotEmpty()
            .WithMessage("A Origem é um campo obrigatório");

        RuleFor(x => x.Destinatario)
            .NotEmpty()
            .WithMessage("Destinatario é um campo obrigatório");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}