using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notification.Adapter.Driven.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cadastro");

            migrationBuilder.CreateTable(
                name: "Not_Notificacao",
                schema: "Cadastro",
                columns: table => new
                {
                    Not_NotificacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Not_Titulo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Not_Corpo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Not_Status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Not_Notificacao", x => x.Not_NotificacaoId);
                });

            migrationBuilder.CreateTable(
                name: "Noa_NotificacaoAuditoria",
                schema: "Cadastro",
                columns: table => new
                {
                    Noa_NotificacaoAuditoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Noa_Destinatario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Noa_Origem = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Noa_DataEnvio = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Not_NotificacaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noa_NotificacaoAuditoria", x => x.Noa_NotificacaoAuditoriaId);
                    table.ForeignKey(
                        name: "FK_Noa_NotificacaoAuditoria_Not_Notificacao_Not_NotificacaoId",
                        column: x => x.Not_NotificacaoId,
                        principalSchema: "Cadastro",
                        principalTable: "Not_Notificacao",
                        principalColumn: "Not_NotificacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Noa_NotificacaoAuditoria_Not_NotificacaoId",
                schema: "Cadastro",
                table: "Noa_NotificacaoAuditoria",
                column: "Not_NotificacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Noa_NotificacaoAuditoria",
                schema: "Cadastro");

            migrationBuilder.DropTable(
                name: "Not_Notificacao",
                schema: "Cadastro");
        }
    }
}
