using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberCode.Infra.Migrations
{
    /// <inheritdoc />
    public partial class corrigindorelacaoservicoBarbearia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId1",
                table: "servicos");

            migrationBuilder.DropIndex(
                name: "IX_servicos_BarbeariaId1",
                table: "servicos");

            migrationBuilder.DropColumn(
                name: "BarbeariaId1",
                table: "servicos");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarbeariaId",
                table: "servicos",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_BarbeariaId",
                table: "servicos",
                column: "BarbeariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId",
                table: "servicos",
                column: "BarbeariaId",
                principalTable: "barbearias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId",
                table: "servicos");

            migrationBuilder.DropIndex(
                name: "IX_servicos_BarbeariaId",
                table: "servicos");

            migrationBuilder.AlterColumn<int>(
                name: "BarbeariaId",
                table: "servicos",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BarbeariaId1",
                table: "servicos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_BarbeariaId1",
                table: "servicos",
                column: "BarbeariaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_servicos_barbearias_BarbeariaId1",
                table: "servicos",
                column: "BarbeariaId1",
                principalTable: "barbearias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
