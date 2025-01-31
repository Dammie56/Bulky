using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Lagos", "Tech Solution", "08122116302", "121213", "Lag", "15, Micheal Shosanya, welder bustop, Ikotun Lagos." },
                    { 2, "Akure", "MachineTech Solution", "08122116302", "66677776", "Ondo", "15, Micheal Shosanya, welder bustop, Ikotun Lagos." },
                    { 3, "Bariga", "Big Baby Solution", "08122116302", "221113", "Lagos", "15, Micheal Shosanya, welder bustop, Ikotun Lagos." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
