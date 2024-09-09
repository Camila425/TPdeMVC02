using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPdeEFCore01.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CrearRelacionEntreShoesSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shoeSizes",
                columns: table => new
                {
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ShoeSizeId = table.Column<int>(type: "int", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoeSizes", x => new { x.ShoeId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_shoeSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shoeSizes_shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoeSizes_ShoeId_SizeId",
                table: "shoeSizes",
                columns: new[] { "ShoeId", "SizeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shoeSizes_SizeId",
                table: "shoeSizes",
                column: "SizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoeSizes");
        }
    }
}
