using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class Admin_Controller_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Students_StudId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BorrowDate",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "OverdueDate",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "StudId",
                table: "BorrowedBooks",
                newName: "studentDtoId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BorrowedBooks",
                newName: "bookDtoBookId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_StudId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_studentDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_bookDtoBookId");

            migrationBuilder.AddColumn<int>(
                name: "studentId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookDTO",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OverdueDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDTO", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "StudentDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDTO", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "studentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "studentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "studentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "studentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5,
                column: "studentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6,
                column: "studentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Books_studentId",
                table: "Books",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_studentId",
                table: "Books",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_BookDTO_bookDtoBookId",
                table: "BorrowedBooks",
                column: "bookDtoBookId",
                principalTable: "BookDTO",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_StudentDTO_studentDtoId",
                table: "BorrowedBooks",
                column: "studentDtoId",
                principalTable: "StudentDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_studentId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_BookDTO_bookDtoBookId",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_StudentDTO_studentDtoId",
                table: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "BookDTO");

            migrationBuilder.DropTable(
                name: "StudentDTO");

            migrationBuilder.DropIndex(
                name: "IX_Books_studentId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "studentDtoId",
                table: "BorrowedBooks",
                newName: "StudId");

            migrationBuilder.RenameColumn(
                name: "bookDtoBookId",
                table: "BorrowedBooks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_studentDtoId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_StudId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_bookDtoBookId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_BookId");

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OverdueDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Students_StudId",
                table: "BorrowedBooks",
                column: "StudId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
