using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class PracticeAppDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bookname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OverdueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Bookname", "BorrowedDate", "OverdueDate", "Status", "StudentName" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "A" },
                    { 2, null, null, null, null, "B" },
                    { 3, null, null, null, null, "C" },
                    { 4, null, null, null, null, "D" },
                    { 5, null, null, null, null, "E" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
