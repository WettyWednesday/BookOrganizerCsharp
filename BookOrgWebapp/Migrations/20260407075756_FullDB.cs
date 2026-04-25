using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookOrgWebapp.Migrations
{
    /// <inheritdoc />
    public partial class FullDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Title",
                table: "Title");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Title",
                newName: "Titles");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AlterColumn<string>(
                name: "BookName",
                table: "Titles",
                type: "nvarchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN10",
                table: "Titles",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Titles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailNormal",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailSmall",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Titles",
                table: "Titles",
                column: "ISBN");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    Genre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(13)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.ISBN, x.Genre });
                    table.ForeignKey(
                        name: "FK_BookGenres_Titles_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Titles",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBooks",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBooks", x => new { x.ISBN, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserBooks_Titles_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Titles",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBooks_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(13)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.ISBN, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Titles_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Titles",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorID", "AuthorName" },
                values: new object[,]
                {
                    { 1, "Katy Brent" },
                    { 2, "Jenny Lawson" },
                    { 3, "Fergus Craig" },
                    { 4, "Ashely Alker" }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "Genre", "ISBN" },
                values: new object[,]
                {
                    { "Fiction", "9780008536695" },
                    { "Biography & Autobiography", "9780425261019" },
                    { "Health & Fitness", "9781250359643" },
                    { "Fiction", "9798217189052" }
                });

            migrationBuilder.UpdateData(
                table: "Titles",
                keyColumn: "ISBN",
                keyValue: "9780008536695",
                columns: new[] { "Description", "ISBN10", "Language", "PageCount", "PublishedDate", "Publisher", "SubTitle", "ThumbnailNormal", "ThumbnailSmall" },
                values: new object[] { "Meet Kitty Collins. FRIEND. LOVER. KILLER.", "0008536694", "en", 0, new DateTime(2023, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "HQ Digital", null, "http://books.google.com/books/content?id=3XZSzwEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api", "http://books.google.com/books/content?id=3XZSzwEACAAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api" });

            migrationBuilder.UpdateData(
                table: "Titles",
                keyColumn: "ISBN",
                keyValue: "9780425261019",
                columns: new[] { "Description", "ISBN10", "Language", "PageCount", "PublishedDate", "Publisher", "SubTitle", "ThumbnailNormal", "ThumbnailSmall" },
                values: new object[] { "The #1 New York Times bestselling (mostly true) memoir from the hilarious author of Furiously Happy. “Gaspingly funny and wonderfully inappropriate.”—O, The Oprah Magazine When Jenny Lawson was little, all she ever wanted was to fit in. That dream was cut short by her fantastically unbalanced father and a morbidly eccentric childhood. It did, however, open up an opportunity for Lawson to find the humor in the strange shame-spiral that is her life, and we are all the better for it. In the irreverent Let’s Pretend This Never Happened, Lawson’s long-suffering husband and sweet daughter help her uncover the surprising discovery that the most terribly human moments—the ones we want to pretend never happened—are the very same moments that make us the people we are today. For every intellectual misfit who thought they were the only ones to think the things that Lawson dares to say out loud, this is a poignant and hysterical look at the dark, disturbing, yet wonderful moments of our lives. Readers Guide Inside", "0425261018", "en", 385, new DateTime(2013, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Penguin", "A Mostly True Memoir", "http://books.google.com/books/content?id=XVSLDQAAQBAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api", "http://books.google.com/books/content?id=XVSLDQAAQBAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api" });

            migrationBuilder.UpdateData(
                table: "Titles",
                keyColumn: "ISBN",
                keyValue: "9781250359643",
                columns: new[] { "Description", "ISBN10", "Language", "PageCount", "PublishedDate", "Publisher", "SubTitle", "ThumbnailNormal", "ThumbnailSmall" },
                values: new object[] { "An illuminating, hilarious, and practical guide to 99 of the most terrifying ways to die and how to avoid them from an emergency medicine doctor. Dr. Ashely Alker is a self-described death escapologist—or, in more familiar terms, an emergency medicine doctor. She has seen it all, from flesh-eating bacteria to the work of a serial killer to the more mundane but no less deadly, and her work outwitting the end has uniquely prepared her to write this book. Dr. Alker manages to shock readers while making them laugh, educating them on how to outsmart a wide range of deadly situations and conditions. Many of the chapters include stories from her experiences in life and medicine, at times heartwarming, others heartbreaking. Sections include explorations of sex, poison, drugs, biological warfare, disease, animals, crime, the elements, and much more. An Anthony Bourdain-style greatest hits tour of death, 99 Ways to Die is entertaining while it informs. Full of valuable advice and wild stories, this riveting read might just save your life.", "1250359643", "en", 0, new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "St. Martin's Press", "And How to Avoid Them", "http://books.google.com/books/content?id=lZ9eEQAAQBAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api", "http://books.google.com/books/content?id=lZ9eEQAAQBAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api" });

            migrationBuilder.UpdateData(
                table: "Titles",
                keyColumn: "ISBN",
                keyValue: "9798217189052",
                columns: new[] { "Description", "ISBN10", "Language", "PageCount", "PublishedDate", "Publisher", "SubTitle", "ThumbnailNormal", "ThumbnailSmall" },
                values: new object[] { "After a decades-long stint in prison, former serial killer Carol is looking to kick back and relax in her new retirement home...until a fellow resident drops dead and Carol has to prove she actually didn't do it this time.... Carol is delighted to be leaving her tiny prison cell behind to take her place in a luxury retirement home. She's hoping her past as a serial killer won't come to light so she can make a few friends and find some murder-free hobbies. But it's not long before a fellow resident—who happens to be a former police commissioner—drops dead, and Carol's true identity is leaked—making catching up over daily activities of bingo and baking rather awkward. Just her luck, Carol soon realizes that the victim wasn't the only former law enforcement officer at Sheldon Oaks—it's filled to the brim with former cops, barristers, and government representatives, her newfound friends included. And everyone thinks Carol's guilt is a no-brainer, but she is ready to prove them dead wrong...without killing anyone, for once.", null, "en", 273, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Penguin Group", null, "http://books.google.com/books/content?id=DE1nEQAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api", "http://books.google.com/books/content?id=DE1nEQAAQBAJ&printsec=frontcover&img=1&zoom=5&edge=curl&source=gbs_api" });

            migrationBuilder.InsertData(
                table: "UserBooks",
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

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorID", "ISBN" },
                values: new object[,]
                {
                    { 1, "9780008536695" },
                    { 2, "9780425261019" },
                    { 4, "9781250359643" },
                    { 3, "9798217189052" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AuthorName",
                table: "Authors",
                column: "AuthorName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorID",
                table: "BookAuthors",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserID",
                table: "UserBooks",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "UserBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Titles",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "ISBN10",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "ThumbnailNormal",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "ThumbnailSmall",
                table: "Titles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Titles",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.AlterColumn<string>(
                name: "BookName",
                table: "Title",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Title",
                table: "Title",
                column: "ISBN");

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
        }
    }
}
