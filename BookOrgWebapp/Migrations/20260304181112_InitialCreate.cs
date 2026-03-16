using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookOrgWebapp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.ISBN);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => new { x.ISBN, x.UserID });
                    table.ForeignKey(
                        name: "FK_Books_Title_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Title",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Title",
                columns: new[] { "ISBN", "BookName" },
                values: new object[,]
                {
                    { "9780008536695", "How to Kill Men and Get Away With It" },
                    { "9780425261019", "Let's Pretend This Never Happened" },
                    { "9781250359643", "99 Ways to Die: And How to Avoid Them" },
                    { "9798217189052", "I'm Not the Only Murderer in My Retirement Home" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "Jannika@gmail.com", "Annika" },
                    { 2, "Clara@gmail.com", "Clara" },
                    { 3, "Anna@gmail.com", "Anna" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ISBN", "UserID" },
                values: new object[,]
                {
                    { "9780008536695", 1 },
                    { "9780425261019", 1 },
                    { "9780425261019", 2 },
                    { "9780425261019", 3 },
                    { "9781250359643", 2 },
                    { "9798217189052", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserID",
                table: "Books",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
