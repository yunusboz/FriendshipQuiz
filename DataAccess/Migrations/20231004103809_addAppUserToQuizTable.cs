using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addAppUserToQuizTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Quizzes",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Quizzes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: new Guid("ea8b887f-042d-4c56-a034-68845aa34099"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { null, new DateTime(2023, 10, 4, 13, 38, 9, 723, DateTimeKind.Local).AddTicks(5992) });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_AppUserId",
                table: "Quizzes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_AppUserId",
                table: "Quizzes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_AppUserId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_AppUserId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Quizzes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: new Guid("ea8b887f-042d-4c56-a034-68845aa34099"),
                column: "CreatedDate",
                value: new DateTime(2023, 10, 1, 13, 9, 2, 818, DateTimeKind.Local).AddTicks(8760));
        }
    }
}
