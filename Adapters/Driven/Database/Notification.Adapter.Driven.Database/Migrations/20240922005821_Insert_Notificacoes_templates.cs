using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notification.Adapter.Driven.Database.Migrations
{
    /// <inheritdoc />
    public partial class Insert_Notificacoes_templates : Migration
    {
        private static readonly string[] columns = new []
                {
                    "Not_NotificacaoId",
                    "Not_Titulo",
                    "Not_Corpo",
                    "Not_Status"
                };
        private static readonly string[] keyValues = new []
                {
                    "b27ae61b-f158-48f3-a1c3-edcf00d38f8b",
                    "76958cfd-19d2-4ed8-809a-f30be245e1ab",
                    "4e0f7d06-1e05-4895-817c-addffc81bf5e"
                };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Not_Notificacao",
                schema: "Cadastro",
                columns: columns,
                values: new object[,]
                {
                    {
                        Guid.Parse("b27ae61b-f158-48f3-a1c3-edcf00d38f8b"),
                        "Seja Bem Vindo",
                        "Seja muito bem-vindo(a)! Estamos super felizes em tê-lo(a) como cliente. Esperamos que você ame" +
                        " nossos produtos tanto quanto nós.",
                        true
                    },
                    {
                        Guid.Parse("76958cfd-19d2-4ed8-809a-f30be245e1ab"),
                        "Atualização Importante: Seus dados conosco",
                        "Gostaríamos de informar que atualizamos seus dados em nosso sistema. Essa atualização tem como objetivo" +
                        " garantir um atendimento ainda mais personalizado e eficiente.",
                        true
                    },
                    {
                        Guid.Parse("4e0f7d06-1e05-4895-817c-addffc81bf5e"),
                        "Exclusão de Cadastro",
                        "Estamos entrando em contato para informar que houve a remoção dos dados cadastrais em nossos serviços.",
                        true
                    }
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Noa_NotificacaoAuditoria",
                schema: "Cadastro",
                keyColumn: "Not_NotificacaoId",
                keyValues: keyValues);
            
            migrationBuilder.DeleteData(table: "Not_Notificacao",
                schema: "Cadastro",
                keyColumn: "Not_NotificacaoId",
                keyValues: keyValues);
        }
    }
}
