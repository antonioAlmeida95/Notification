using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Core.Domain.Entities;

namespace Notification.Adapter.Driven.Database.Mappings;

public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Not_NotificacaoId");
        
        builder.Property(x => x.Titulo)
            .HasColumnName("Not_Titulo")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.Corpo)
            .HasColumnName("Not_Corpo")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.Status)
            .HasColumnName("Not_Status")
            .HasDefaultValue(true);

        builder.HasMany(s => s.NotificacoesAuditoria)
            .WithOne(s => s.Notificacao)
            .HasForeignKey(s => s.NotificacaoId);
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Not_Notificacao", "Cadastro");
    }
}