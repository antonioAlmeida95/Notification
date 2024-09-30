using FluentValidation;

namespace Notification.Core.Domain.Entities;

public class Notificacao : EntidadeBase<Notificacao>
{
    public string? Titulo { get; }
    public string? Corpo { get; }
    public bool Status { get; private set; }
    public virtual IEnumerable<NotificacaoAuditoria> NotificacoesAuditoria { get; } = [];
    
    public void AlterarStatus(bool status) => Status = status;
    
    public Notificacao(){}

    public Notificacao(string? titulo, string? corpo, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Titulo = titulo;
        Corpo = corpo;
    }
    
    public Notificacao(string? titulo, string? corpo)
        : this(titulo, corpo, null) { }

    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Corpo)
            .NotEmpty()
            .WithMessage("Corpo é um campo obrigatório");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Titulo é um campo obrigatório");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}