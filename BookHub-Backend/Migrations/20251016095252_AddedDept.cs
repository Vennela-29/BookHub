using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedDept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Department", "Year" },
                values: new object[] { "CSE", 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Department", "Year" },
                values: new object[] { "ECE", 3 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Department", "Year" },
                values: new object[] { "EEE", 2 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Department", "Year" },
                values: new object[] { "MECH", 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Department", "Year" },
                values: new object[] { "CSE", 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Students");
        }
    }
}
