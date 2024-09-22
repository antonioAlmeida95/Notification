using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Notification.Core.Domain.Entities;

public abstract class EntidadeBase<T> : AbstractValidator<T> where T : EntidadeBase<T>
{
    [Required]
    public Guid Id { get; protected set; }
    
    [NotMapped]
    public ValidationResult ValidationResult { get; protected set; }
    
    protected EntidadeBase()
    {
        ValidationResult = new ValidationResult();
    }
    
    public abstract bool ValidarEntidade();
}