using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api1.Migrations
{
    /// <inheritdoc />
    public partial class ModificoOrden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PrecioTotalCompra",
                table: "Orden",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioTotalCompra",
                table: "Orden");
        }
    }
}
