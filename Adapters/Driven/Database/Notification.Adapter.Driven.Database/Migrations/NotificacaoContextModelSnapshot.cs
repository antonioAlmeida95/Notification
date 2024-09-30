﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notification.Adapter.Driven.Database.UnitOfWork;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notification.Adapter.Driven.Database.Migrations
{
    [DbContext(typeof(NotificacaoContext))]
    partial class NotificacaoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notification.Core.Domain.Entities.Notificacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Not_NotificacaoId");

                    b.Property<string>("Corpo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("Not_Corpo");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("Not_Status");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Not_Titulo");

                    b.HasKey("Id");

                    b.ToTable("Not_Notificacao", "Cadastro");
                });

            modelBuilder.Entity("Notification.Core.Domain.Entities.NotificacaoAuditoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Noa_NotificacaoAuditoriaId");

                    b.Property<DateTimeOffset>("DataEnvio")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Noa_DataEnvio");

                    b.Property<string>("Destinatario")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Noa_Destinatario");

                    b.Property<Guid>("NotificacaoId")
                        .HasColumnType("uuid")
                        .HasColumnName("Not_NotificacaoId");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Noa_Origem");

                    b.HasKey("Id");

                    b.HasIndex("NotificacaoId");

                    b.ToTable("Noa_NotificacaoAuditoria", "Cadastro");
                });

            modelBuilder.Entity("Notification.Core.Domain.Entities.NotificacaoAuditoria", b =>
                {
                    b.HasOne("Notification.Core.Domain.Entities.Notificacao", "Notificacao")
                        .WithMany("NotificacoesAuditoria")
                        .HasForeignKey("NotificacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notificacao");
                });

            modelBuilder.Entity("Notification.Core.Domain.Entities.Notificacao", b =>
                {
                    b.Navigation("NotificacoesAuditoria");
                });
#pragma warning restore 612, 618
        }
    }
}
