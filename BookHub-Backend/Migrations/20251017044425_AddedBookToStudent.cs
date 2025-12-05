using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4288), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4295) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4316), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4317) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4319), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4319) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4321) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 44, 24, 792, DateTimeKind.Utc).AddTicks(4322), new DateTime(2025, 10, 27, 10, 14, 24, 792, DateTimeKind.Local).AddTicks(4322) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BorrowedDate", "OverdueDate" },
                values: new object[] { null, null });
        }
    }
}
