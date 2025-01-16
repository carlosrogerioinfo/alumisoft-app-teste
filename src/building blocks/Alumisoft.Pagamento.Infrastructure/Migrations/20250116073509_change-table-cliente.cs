using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumisoft.Pagamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changetablecliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "alumisoft_clientes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "alumisoft_clientes",
                newName: "Ativo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "alumisoft_clientes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "alumisoft_clientes",
                newName: "Active");
        }
    }
}
