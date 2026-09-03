using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naringskollen.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNamesOnCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "90cc432c-f9a7-4b24-ad6c-8fa565c0b489");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Fetter & oljor");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Färdiga rätter, såser & snabbmat");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Skafferi, smaksättare & kryddor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5df68d82-9384-4f04-b303-960447f8c70a");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Fetter, oljor & såser");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Färdiga rätter & snabbmat");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Skafferi & kryddor");
        }
    }
}
