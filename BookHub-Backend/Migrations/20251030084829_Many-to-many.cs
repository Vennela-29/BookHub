using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class Manytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_StudentId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "StudentDTOId",
                table: "BookDTO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "StudentBooks",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBooks", x => new { x.BooksBookId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentBooks_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentBooks_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDTO_StudentDTOId",
                table: "BookDTO",
                column: "StudentDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBooks_StudentsId",
                table: "StudentBooks",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDTO_StudentDTO_StudentDTOId",
                table: "BookDTO",
                column: "StudentDTOId",
                principalTable: "StudentDTO",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDTO_StudentDTO_StudentDTOId",
                table: "BookDTO");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "StudentBooks");

            migrationBuilder.DropIndex(
                name: "IX_BookDTO_StudentDTOId",
                table: "BookDTO");

            migrationBuilder.DropColumn(
                name: "StudentDTOId",
                table: "BookDTO");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "StudentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "StudentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "StudentId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "StudentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5,
                column: "StudentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6,
                column: "StudentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Books_StudentId",
                table: "Books",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
