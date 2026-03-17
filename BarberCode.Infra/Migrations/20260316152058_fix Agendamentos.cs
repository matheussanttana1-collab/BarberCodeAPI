using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberCode.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fixAgendamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "barbearias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco_Lougradouro = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco_Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco_Numero = table.Column<int>(type: "int", nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco_Estado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barbearias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "barbeiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FotoPerfil = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BarbeariaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HorarioAlmoco = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barbeiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_barbeiros_barbearias_BarbeariaId",
                        column: x => x.BarbeariaId,
                        principalTable: "barbearias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HorarioFuncionamento",
                columns: table => new
                {
                    BarbeariaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dia = table.Column<int>(type: "int", nullable: false),
                    Incio = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    Fim = table.Column<TimeOnly>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioFuncionamento", x => new { x.BarbeariaId, x.Id });
                    table.ForeignKey(
                        name: "FK_HorarioFuncionamento_barbearias_BarbeariaId",
                        column: x => x.BarbeariaId,
                        principalTable: "barbearias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "servicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BarbeariaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_servicos_barbearias_BarbeariaId",
                        column: x => x.BarbeariaId,
                        principalTable: "barbearias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "agendamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BarbeiroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BarbeariaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Cliente_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cliente_Phone = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<DateOnly>(type: "date", nullable: false),
                    Horario = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    ServicoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agendamentos_barbearias_BarbeariaId",
                        column: x => x.BarbeariaId,
                        principalTable: "barbearias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agendamentos_barbeiros_BarbeiroId",
                        column: x => x.BarbeiroId,
                        principalTable: "barbeiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agendamentos_servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_BarbeariaId",
                table: "agendamentos",
                column: "BarbeariaId");

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_BarbeiroId",
                table: "agendamentos",
                column: "BarbeiroId");

        
            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_ServicoId",
                table: "agendamentos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_barbeiros_BarbeariaId",
                table: "barbeiros",
                column: "BarbeariaId");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_BarbeariaId",
                table: "servicos",
                column: "BarbeariaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamentos");

            migrationBuilder.DropTable(
                name: "HorarioFuncionamento");

            migrationBuilder.DropTable(
                name: "barbeiros");

            migrationBuilder.DropTable(
                name: "servicos");

            migrationBuilder.DropTable(
                name: "barbearias");
        }
    }
}
