using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Theater_Model.Migrations
{
    /// <inheritdoc />
    public partial class FixPrimaryKeyScreen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_ScreenTime",
                table: "ScreenTime",
                column: "Screening_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScreenTime",
                table: "ScreenTime");
        }
    }
}
