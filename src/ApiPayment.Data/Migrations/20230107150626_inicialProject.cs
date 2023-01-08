using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPayment.Data.Migrations
{
    /// <inheritdoc />
    public partial class inicialProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItensVenda",
                columns: table => new
                {
                    Produto = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "TEXT", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensVenda", x => x.Produto);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    NumeroPedido = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Vendedor = table.Column<int>(type: "INTEGER", nullable: false),
                    Itens = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.NumeroPedido);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cpf = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_Cpf",
                table: "Vendedores",
                column: "Cpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensVenda");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Vendedores");
        }
    }
}
