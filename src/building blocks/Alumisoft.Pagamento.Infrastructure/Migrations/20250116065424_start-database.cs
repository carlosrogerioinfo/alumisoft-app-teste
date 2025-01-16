using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumisoft.Pagamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class startdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumisoft_clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumisoft_clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alumisoft_clientes_Email",
                table: "alumisoft_clientes",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alumisoft_clientes");
        }
    }
}
