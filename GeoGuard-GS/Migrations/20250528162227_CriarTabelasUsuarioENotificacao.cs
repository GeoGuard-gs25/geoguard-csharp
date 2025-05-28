using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuard_GS.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelasUsuarioENotificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RM554694");

            migrationBuilder.CreateTable(
                name: "USUARIO",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RECEBER_NOTIFICACOES = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACAO",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TITULO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    MENSAGEM = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATA_ENVIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    USUARIO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalSchema: "RM554694",
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_USUARIO_ID",
                schema: "RM554694",
                table: "NOTIFICACAO",
                column: "USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTIFICACAO",
                schema: "RM554694");

            migrationBuilder.DropTable(
                name: "USUARIO",
                schema: "RM554694");
        }
    }
}
