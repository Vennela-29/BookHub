using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class Borrowed_Table_Added : Migration
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
                name: "Bookname",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BorrowedDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OverdueDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AvailableCopies",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OverdueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Students_StudId",
                        column: x => x.StudId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Phone", "StudentName" },
                values: new object[] { "Akash@gmail.com", "9876543212", "Akash" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Phone", "StudentName" },
                values: new object[] { "Goutham@gmail.com", "9321456708", "Goutham" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Phone", "StudentName" },
                values: new object[] { "Karthik@gmail.com", "8759543212", "Karthik" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Phone", "StudentName" },
                values: new object[] { "Darshan@gmail.com", "9892826312", "Darshan" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Phone", "StudentName" },
                values: new object[] { "Arjun@gmail.com", "9874853212", "Arjun" });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_StudId",
                table: "BorrowedBooks",
                column: "StudId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AvailableCopies",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Bookname",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowedDate",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OverdueDate",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

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
                value: 3);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "StudentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "StudentId",
                value: 2);

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
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6,
                column: "StudentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bookname", "BorrowedDate", "OverdueDate", "Status", "StudentName" },
                values: new object[] { null, new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4288), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4295), null, "A" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bookname", "BorrowedDate", "OverdueDate", "Status", "StudentName" },
                values: new object[] { null, new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4316), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4317), null, "B" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bookname", "BorrowedDate", "OverdueDate", "Status", "StudentName" },
                values: new object[] { null, new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4319), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4319), null, "C" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Bookname", "BorrowedDate", "OverdueDate", "Status", "StudentName" },
                values: new object[] { null, new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4321), null, "D" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Bookname", "BorrowedDate", "OverdueDate", "Status", "StudentName" },
                values: new object[] { null, new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4322), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4322), null, "E" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_StudentId",
                table: "Books",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
