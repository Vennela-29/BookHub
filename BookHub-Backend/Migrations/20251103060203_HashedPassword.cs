using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class HashedPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$dxuext4uhGVzGXagEEokAuubco82PFGJ/r3NUBlAGGfZbYKPAnvVa");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ihw92xRpkLKOAWEeiTyFGudhXZwWRAChINOS46/iUKL7UYDqTKU4y");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$8fAxrH35.wJ2T9nOUKXnKu/68M1yNMqijLsGInsKiVXqprG0R61ei");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$e/qVQIU.yBbKE2kyf1X1Q.eDU83zIgIVW7P6noboxvgrSdq3EuMIG");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$RPk4GlH4T4JHcDLg.kfL8uts1iA31pzjQ9g6NlXm5PD0D7mTQUywm");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$1dZ2f7RNRvpcZg8cQDwPJOOmU8Q9/MDW9UjEiBjAy3bYOgtx5Kxee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
