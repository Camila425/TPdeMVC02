using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPdeEFCore01.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeRelacionEntreColorYShoe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "shoes",
                keyColumn: "ShoeId",
                keyValue: 1,
                column: "ColorId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "shoes",
                keyColumn: "ShoeId",
                keyValue: 2,
                column: "ColorId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "shoes",
                keyColumn: "ShoeId",
                keyValue: 3,
                column: "ColorId",
                value: 0);
            migrationBuilder.Sql("UPDATE shoes SET ColorId=1");


            migrationBuilder.CreateIndex(
                name: "IX_shoes_ColorId",
                table: "shoes",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_shoes_Colors_ColorId",
                table: "shoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoes_Colors_ColorId",
                table: "shoes");

            migrationBuilder.DropIndex(
                name: "IX_shoes_ColorId",
                table: "shoes");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "shoes");
        }
    }
}
