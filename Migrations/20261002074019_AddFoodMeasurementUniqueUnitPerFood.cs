using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naringskollen.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodMeasurementUniqueUnitPerFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodMeasurements_FoodId",
                table: "FoodMeasurements");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "FoodMeasurements",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMeasurements_FoodId_Unit",
                table: "FoodMeasurements",
                columns: new[] { "FoodId", "Unit" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodMeasurements_FoodId_Unit",
                table: "FoodMeasurements");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "FoodMeasurements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.CreateIndex(
                name: "IX_FoodMeasurements_FoodId",
                table: "FoodMeasurements",
                column: "FoodId");
        }
    }
}
