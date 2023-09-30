using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizResultToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: true),
                    WrongAnswer = table.Column<int>(type: "int", nullable: true),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizResults_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: new Guid("ea8b887f-042d-4c56-a034-68845aa34099"),
                column: "CreatedDate",
                value: new DateTime(2023, 9, 30, 17, 10, 31, 782, DateTimeKind.Local).AddTicks(2191));

            migrationBuilder.CreateIndex(
                name: "IX_QuizResults_QuizId",
                table: "QuizResults",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizResults");

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: new Guid("ea8b887f-042d-4c56-a034-68845aa34099"),
                column: "CreatedDate",
                value: new DateTime(2023, 9, 30, 14, 0, 14, 852, DateTimeKind.Local).AddTicks(9097));
        }
    }
}
