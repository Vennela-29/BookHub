using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "BookName", "StudentId" },
                values: new object[,]
                {
                    { 1, "Valmiki", "Ramayan", 3 },
                    { 2, "Anne Frank", "Diary of a Young girl", 1 },
                    { 3, "Sudha Murthy", "House of cards", 2 },
                    { 4, "A.P.J Abdul Kalam", "Wings of Fire", null },
                    { 5, "Shiv Khera", "You can win", 5 },
                    { 6, "Veda Vyas", "Mahabharat", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_StudentId",
                table: "Books",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
