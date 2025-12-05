using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApp.Migrations
{
    /// <inheritdoc />
    public partial class BorrowedBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentBooks");

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false),
                    BorrowedOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverdueOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BorrowedBooks_bookId",
                table: "BorrowedBooks",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_studentId",
                table: "BorrowedBooks",
                column: "studentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.CreateTable(
                name: "StudentBooks",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBooks", x => new { x.BooksBookId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentBooks_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentBooks_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$QTNpTy5SFs9GMXi/hKsgdexiIB0T.CFhtWhe6ZhCFzyLoTKkYurOG");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$gThePRh1cGBa/4dmDoQdcuhemwV8yCrBzf6s8vsKN1Knwm99PcJEG");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$O.OwsjXNo/g4Ft3pl3lp3ehex7/V6JvKh1.sVBrS5P3vNy4hYdJnq");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$4zQsBbUpCmADARNRgFQ05OxYSLPFe62sa88CcwkbuU03F.nECTnGq");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$37dnEvdYfUJJayW62d/iiOerx6oHBBozSw6o1vbTcO6CfHmhgtzx2");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$9X1GoJxJN.2rumTSKLkFoeEO9uujLC0OrDWNO/2l1ZMlxWvYU7JR2");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBooks_StudentsId",
                table: "StudentBooks",
                column: "StudentsId");
        }
    }
}
