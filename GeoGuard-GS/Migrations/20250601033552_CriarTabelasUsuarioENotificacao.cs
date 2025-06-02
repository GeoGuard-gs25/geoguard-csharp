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
                name: "TB_USUARIO",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    LOCALIZACAO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_NOTIFICACAO",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TITULO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    MENSAGEM = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    TIPO_MENSAGEM = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DATA_ENVIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    USUARIO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NOTIFICACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_NOTIFICACAO_TB_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalSchema: "RM554694",
                        principalTable: "TB_USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_NOTIFICACAO_USUARIO_ID",
                schema: "RM554694",
                table: "TB_NOTIFICACAO",
                column: "USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_NOTIFICACAO",
                schema: "RM554694");

            migrationBuilder.DropTable(
                name: "TB_USUARIO",
                schema: "RM554694");
        }
    }
}
