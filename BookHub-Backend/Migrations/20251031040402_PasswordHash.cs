using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "BookDTO");

            migrationBuilder.DropTable(
                name: "StudentDTO");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "Admin123");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "Ak2345");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "GM2345");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "KT2345");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "DS2345");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "AJ2345");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admin");

            migrationBuilder.CreateTable(
                name: "StudentDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookDTO",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OverdueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudentDTOId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDTO", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookDTO_StudentDTO_StudentDTOId",
                        column: x => x.StudentDTOId,
                        principalTable: "StudentDTO",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookDtoBookId = table.Column<int>(type: "int", nullable: false),
                    studentDtoId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_BookDTO_bookDtoBookId",
                        column: x => x.bookDtoBookId,
                        principalTable: "BookDTO",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_StudentDTO_studentDtoId",
                        column: x => x.studentDtoId,
                        principalTable: "StudentDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDTO_StudentDTOId",
                table: "BookDTO",
                column: "StudentDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_bookDtoBookId",
                table: "BorrowedBooks",
                column: "bookDtoBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_studentDtoId",
                table: "BorrowedBooks",
                column: "studentDtoId");
        }
    }
}
