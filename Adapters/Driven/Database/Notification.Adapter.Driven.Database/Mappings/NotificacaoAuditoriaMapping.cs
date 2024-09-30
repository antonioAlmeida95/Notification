using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Core.Domain.Entities;

namespace Notification.Adapter.Driven.Database.Mappings;

public class NotificacaoAuditoriaMapping : IEntityTypeConfiguration<NotificacaoAuditoria>
{
    public void Configure(EntityTypeBuilder<NotificacaoAuditoria> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Noa_NotificacaoAuditoriaId");
        
        builder.Property(x => x.NotificacaoId)
            .HasColumnName("Not_NotificacaoId")
            .IsRequired();
        
        builder.Property(x => x.Destinatario)
            .HasColumnName("Noa_Destinatario")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Origem)
            .HasColumnName("Noa_Origem")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(x => x.DataEnvio)
            .HasColumnName("Noa_DataEnvio")
            .IsRequired();
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Noa_NotificacaoAuditoria", "Cadastro");
    }
}