using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naringskollen.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "90cc432c-f9a7-4b24-ad6c-8fa565c0b489", "Admin", "ADMIN" });
        }
    }
}
