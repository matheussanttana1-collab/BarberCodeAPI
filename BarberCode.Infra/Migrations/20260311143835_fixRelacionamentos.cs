using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberCode.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fixRelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agendamentos_barbeiros_BarbeiroId",
                table: "agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_barbeiros_barbearias_BarbeariaId1",
                table: "barbeiros");

            migrationBuilder.DropForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId",
                table: "servicos");

            migrationBuilder.DropTable(
                name: "BarbeiroServico");

            migrationBuilder.DropIndex(
                name: "IX_servicos_BarbeariaId",
                table: "servicos");

            migrationBuilder.DropIndex(
                name: "IX_barbeiros_BarbeariaId1",
                table: "barbeiros");

            migrationBuilder.DropColumn(
                name: "BarbeariaId1",
                table: "barbeiros");

            migrationBuilder.RenameColumn(
                name: "BarbeiroId",
                table: "agendamentos",
                newName: "BarbeiroID");

            migrationBuilder.RenameIndex(
                name: "IX_agendamentos_BarbeiroId",
                table: "agendamentos",
                newName: "IX_agendamentos_BarbeiroID");

            migrationBuilder.AlterColumn<int>(
                name: "BarbeariaId",
                table: "servicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BarbeariaId1",
                table: "servicos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BarbeariaId",
                table: "barbeiros",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "HorarioAlmoco",
                table: "barbeiros",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "barbeiros",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cidade",
                table: "barbearias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Estado",
                table: "barbearias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Lougradouro",
                table: "barbearias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Nome",
                table: "barbearias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Endereco_Numero",
                table: "barbearias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "barbearias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarbeiroID",
                table: "agendamentos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BarbeariaID",
                table: "agendamentos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ServicoId",
                table: "agendamentos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_BarbeariaId1",
                table: "servicos",
                column: "BarbeariaId1");

            migrationBuilder.CreateIndex(
                name: "IX_barbeiros_BarbeariaId",
                table: "barbeiros",
                column: "BarbeariaId");

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_BarbeariaID",
                table: "agendamentos",
                column: "BarbeariaID");

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_ServicoId",
                table: "agendamentos",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_agendamentos_barbearias_BarbeariaID",
                table: "agendamentos",
                column: "BarbeariaID",
                principalTable: "barbearias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agendamentos_barbeiros_BarbeiroID",
                table: "agendamentos",
                column: "BarbeiroID",
                principalTable: "barbeiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agendamentos_servicos_ServicoId",
                table: "agendamentos",
                column: "ServicoId",
                principalTable: "servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_barbeiros_barbearias_BarbeariaId",
                table: "barbeiros",
                column: "BarbeariaId",
                principalTable: "barbearias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId1",
                table: "servicos",
                column: "BarbeariaId1",
                principalTable: "barbearias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agendamentos_barbearias_BarbeariaID",
                table: "agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_agendamentos_barbeiros_BarbeiroID",
                table: "agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_agendamentos_servicos_ServicoId",
                table: "agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_barbeiros_barbearias_BarbeariaId",
                table: "barbeiros");

            migrationBuilder.DropForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId1",
                table: "servicos");

            migrationBuilder.DropIndex(
                name: "IX_servicos_BarbeariaId1",
                table: "servicos");

            migrationBuilder.DropIndex(
                name: "IX_barbeiros_BarbeariaId",
                table: "barbeiros");

            migrationBuilder.DropIndex(
                name: "IX_agendamentos_BarbeariaID",
                table: "agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_agendamentos_ServicoId",
                table: "agendamentos");

            migrationBuilder.DropColumn(
                name: "BarbeariaId1",
                table: "servicos");

            migrationBuilder.DropColumn(
                name: "BarbeariaId",
                table: "barbeiros");

            migrationBuilder.DropColumn(
                name: "HorarioAlmoco",
                table: "barbeiros");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "barbeiros");

            migrationBuilder.DropColumn(
                name: "Endereco_Cidade",
                table: "barbearias");

            migrationBuilder.DropColumn(
                name: "Endereco_Estado",
                table: "barbearias");

            migrationBuilder.DropColumn(
                name: "Endereco_Lougradouro",
                table: "barbearias");

            migrationBuilder.DropColumn(
                name: "Endereco_Nome",
                table: "barbearias");

            migrationBuilder.DropColumn(
                name: "Endereco_Numero",
                table: "barbearias");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "barbearias");

            migrationBuilder.DropColumn(
                name: "BarbeariaID",
                table: "agendamentos");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "agendamentos");

            migrationBuilder.RenameColumn(
                name: "BarbeiroID",
                table: "agendamentos",
                newName: "BarbeiroId");

            migrationBuilder.RenameIndex(
                name: "IX_agendamentos_BarbeiroID",
                table: "agendamentos",
                newName: "IX_agendamentos_BarbeiroId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarbeariaId",
                table: "servicos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "BarbeariaId1",
                table: "barbeiros",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarbeiroId",
                table: "agendamentos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "BarbeiroServico",
                columns: table => new
                {
                    BarbeirosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServicosPrestadosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarbeiroServico", x => new { x.BarbeirosId, x.ServicosPrestadosId });
                    table.ForeignKey(
                        name: "FK_BarbeiroServico_barbeiros_BarbeirosId",
                        column: x => x.BarbeirosId,
                        principalTable: "barbeiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarbeiroServico_servicos_ServicosPrestadosId",
                        column: x => x.ServicosPrestadosId,
                        principalTable: "servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_BarbeariaId",
                table: "servicos",
                column: "BarbeariaId");

            migrationBuilder.CreateIndex(
                name: "IX_barbeiros_BarbeariaId1",
                table: "barbeiros",
                column: "BarbeariaId1");

            migrationBuilder.CreateIndex(
                name: "IX_BarbeiroServico_ServicosPrestadosId",
                table: "BarbeiroServico",
                column: "ServicosPrestadosId");

            migrationBuilder.AddForeignKey(
                name: "FK_agendamentos_barbeiros_BarbeiroId",
                table: "agendamentos",
                column: "BarbeiroId",
                principalTable: "barbeiros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_barbeiros_barbearias_BarbeariaId1",
                table: "barbeiros",
                column: "BarbeariaId1",
                principalTable: "barbearias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId",
                table: "servicos",
                column: "BarbeariaId",
                principalTable: "barbearias",
                principalColumn: "Id");
        }
    }
}
