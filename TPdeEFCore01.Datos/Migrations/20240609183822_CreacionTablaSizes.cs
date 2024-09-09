using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPdeEFCore01.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeNumber = table.Column<double>(type: "decimal(3,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_SizeNumber",
                table: "Sizes",
                column: "SizeNumber",
                unique: true);
            // Agrego talles del 28 al 50 aumentando de a 0.5
            for (decimal size = 28; size <= 50; size += 0.5m)
            {
                migrationBuilder.InsertData(
                    table: "Sizes",
                    columns: new[] { "SizeNumber" },
                    values: new object[] { size });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sizes");
        }
    }
}
