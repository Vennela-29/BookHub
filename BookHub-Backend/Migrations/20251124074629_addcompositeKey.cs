using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class addcompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks",
                columns: new[] { "studentId", "bookId" });

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$0gQpStngzvgzBEPejIKjmuXiOaNhII4pyR.48ckW1WRrYXfpN6kBW");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$oBT5dWEC4Nnw.SpBRlEz5u133eY1RzIP.ta6OMJiKvq3wYiUagcR2");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$83clkCnrFj4bJuVu5W90N.zseY1hKm2PjFBbdpLTP1M.AgxmMe4Jq");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$QbxAJ/AFk8Ev2kzRar15y.52sXsqeq3kTqNqGOhYioIcwwd0Vhf6i");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$zYyb.mYj.WERYDmrx38Z8OpddE/YufFePtwvt7XH.oX59Ou2CPUTW");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$9aq.Oy9u9Kaab8DcUfy4H.N8mQ8/XfCXWKrhoW8.5QOPDw79qcykG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowedBooks",
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
    }
}
