using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBorrowID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_studentId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BorrowId",
                table: "BorrowedBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks",
                column: "studentId");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$KFxp2FzKCeTd.NKbKQoI/u7dq2gmHDL5nFFwpkbuOxttcVyHZaORq");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$wF7qpLZuRVu0eQYEDnNwAe7CQSPOCmG2alLpR.cy4.3V8E/Ty.DTG");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$RIiM9FAMzPluiY/RG3qcqulTYWAy7kN/rFVturL7DtEnFaQN9HHtC");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$MT1AP9CVkS30drDN.A43cO7wFrW8GHFOiRzbf7DmxfNLvqxVfkjVS");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$tbkXmlPrX0JWIhj8PZXGXen2IkmAvOqogLHQKjGQmpugMdVvU8cRW");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$kSjnifkQgJNvea2R5bEkaesNJlh7iF/y/72t9onWsd0qbJXUvCHny");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks");

            migrationBuilder.AddColumn<int>(
                name: "BorrowId",
                table: "BorrowedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks",
                column: "BorrowId");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$1SN7OrcSK9PC8r9ub7A6fO77il8.xxOzl/MyjolSFBX0LbXRpQm66");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$xY8Qejm9yMhsuKJPyMgM0u1YeAnyTQLM5nS.6irf.NOoy3e45J3ku");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$g0IfIQZAEz3mwB1njzl99O2VHRjZ0xrYzj/Ug6HUkBOdVFhdbw7PS");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$HZut1rIxhQqGFQkpPbxgW.eUFbH4sxMBfIv6DSSpa9.U5tZIu90Ya");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$MxbbMBibzWfnBuFHbnpuPOh5EKi6d664Ds5YkgzRsD5W.hjvBEuGS");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$S8ZC6Ggbl2P3tP5CGq9Oi.oRBHZ8YmkzA9yRBXquNyWB9neWXCwe6");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_studentId",
                table: "BorrowedBooks",
                column: "studentId");
        }
    }
}
