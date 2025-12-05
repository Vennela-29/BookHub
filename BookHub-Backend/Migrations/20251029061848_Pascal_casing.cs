using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class Pascal_casing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_studentId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Books",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_studentId",
                table: "Books",
                newName: "IX_Books_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Books",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_StudentId",
                table: "Books",
                newName: "IX_Books_studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_studentId",
                table: "Books",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
