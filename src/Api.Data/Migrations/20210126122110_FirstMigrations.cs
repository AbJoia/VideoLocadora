using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class FirstMigrations : Migration
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
                    TipoUsuario = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(maxLength: 100, nullable: true),
                    Matricula = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aluguel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    DataDevolucao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluguel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aluguel_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    FuncionarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_filme_usuario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_aluguel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    AluguelId = table.Column<Guid>(nullable: false),
                    FilmeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_aluguel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_item_aluguel_aluguel_AluguelId",
                        column: x => x.AluguelId,
                        principalTable: "aluguel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_aluguel_filme_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluguel_UsuarioId",
                table: "aluguel",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_filme_FuncionarioId",
                table: "filme",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_item_aluguel_AluguelId",
                table: "item_aluguel",
                column: "AluguelId");

            migrationBuilder.CreateIndex(
                name: "IX_item_aluguel_FilmeId",
                table: "item_aluguel",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Matricula",
                table: "usuario",
                column: "Matricula",
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
                name: "item_aluguel");

            migrationBuilder.DropTable(
                name: "aluguel");

            migrationBuilder.DropTable(
                name: "filme");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
