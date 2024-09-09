using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TPdeEFCore01.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarListaShoesEnTablaShoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "shoes",
                columns: new[] { "ShoeId", "BrandId", "Description", "GenreId", "Model", "Price", "SportId" },
                values: new object[,]
                {
                    { 1, 1, "con tres rayas blancas laterales", 1, "Samba", 8999000m, 1 },
                    { 2, 2, "Zapatillas para correr con tecnología", 2, " Air Max 270", 12999m, 2 },
                    { 3, 3, "Zapatillas de Volleyball", 3, "  clyde all-pro", 10999m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shoes",
                keyColumn: "ShoeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "shoes",
                keyColumn: "ShoeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "shoes",
                keyColumn: "ShoeId",
                keyValue: 3);
        }
    }
}
