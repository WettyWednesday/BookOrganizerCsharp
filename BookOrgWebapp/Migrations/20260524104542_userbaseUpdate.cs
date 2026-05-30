using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookOrgWebapp.Migrations
{
    /// <inheritdoc />
    public partial class userbaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "AvatarUrl", "Email", "GoogleId", "Name" },
                values: new object[,]
                {
                    { 4, null, "nikolajbo@gmail.com", null, "Nikolaj" },
                    { 5, null, "mustafabaker2970@gmail.com", null, "Mustafa" },
                    { 6, null, "pmachalet@gmail.com", null, "Patrick" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6);
        }
    }
}
