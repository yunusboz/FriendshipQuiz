using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesToDbAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizID", "CreatedBy", "CreatedDate", "Name", "VisitLimit" },
                values: new object[] { new Guid("ea8b887f-042d-4c56-a034-68845aa34099"), "Admin", new DateTime(2023, 9, 30, 14, 0, 14, 852, DateTimeKind.Local).AddTicks(9097), "Havuz Soruları", 5 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "CorrectAnswer", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "QuestionText", "QuizID" },
                values: new object[,]
                {
                    { 1, 3, "Patates Kızartması", "Burger", "Döner", "Kuru Fasulye", "Makarna", "En sevdiği yemek?", new Guid("ea8b887f-042d-4c56-a034-68845aa34099") },
                    { 2, 1, "Pop", "Rap", "Rock", "Türk Halk Müziği", "Arabesk", "En sevdiği müzik türü?", new Guid("ea8b887f-042d-4c56-a034-68845aa34099") },
                    { 3, 4, "Uyuyarak", "Bilgisayar başında", "Yürüyüş yaparak", "Kitap okuyarak", "Arkadaşlarıyla buluşarak", "Zamanını nasıl geçirir?", new Guid("ea8b887f-042d-4c56-a034-68845aa34099") },
                    { 4, 2, "Kayıp para bulmak", "Tuttuğu takımın galibiyeti", "Süpriz hediye almak", "Alışveriş mağazasındaki indirimler", "Çekilişle telefon kazanmak", "Onu en çok ne sevindirir?", new Guid("ea8b887f-042d-4c56-a034-68845aa34099") },
                    { 5, 5, "Kırmızı", "Beyaz", "Siyah", "Sarı", "Mavi", "Favori rengi ne?", new Guid("ea8b887f-042d-4c56-a034-68845aa34099") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizID",
                table: "Questions",
                column: "QuizID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
