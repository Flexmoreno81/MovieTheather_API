using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Theater_Model.Migrations
{
    /// <inheritdoc />
    public partial class intital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    director = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    genre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    release_year = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Theather",
                columns: table => new
                {
                    Theather_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    state = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    zipcode = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    seat_capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theather", x => x.Theather_ID);
                });

            migrationBuilder.CreateTable(
                name: "ScreenTime",
                columns: table => new
                {
                    Screening_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: false),
                    Theather_ID = table.Column<int>(type: "int", nullable: false),
                    Screen_time = table.Column<TimeOnly>(type: "time(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ScreenTime_Movies",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "MovieID");
                    table.ForeignKey(
                        name: "FK_ScreenTime_Theather",
                        column: x => x.Theather_ID,
                        principalTable: "Theather",
                        principalColumn: "Theather_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTime_Movie_ID",
                table: "ScreenTime",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTime_Theather_ID",
                table: "ScreenTime",
                column: "Theather_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenTime");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Theather");
        }
    }
}
