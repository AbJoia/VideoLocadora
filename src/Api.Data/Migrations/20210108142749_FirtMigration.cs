using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class FirtMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    tipoUsuario = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    senha = table.Column<string>(maxLength: 100, nullable: true),
                    matricula = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "filme",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 100, nullable: false),
                    Categoria = table.Column<int>(nullable: false),
                    QtdLocacao = table.Column<int>(nullable: false),
                    locatarioId = table.Column<Guid>(nullable: true),
                    cadastradorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_filme_usuario_cadastradorId",
                        column: x => x.cadastradorId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_filme_usuario_locatarioId",
                        column: x => x.locatarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_filme_cadastradorId",
                table: "filme",
                column: "cadastradorId");

            migrationBuilder.CreateIndex(
                name: "IX_filme_locatarioId",
                table: "filme",
                column: "locatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_matricula",
                table: "usuario",
                column: "matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Email",
                table: "usuario",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filme");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
