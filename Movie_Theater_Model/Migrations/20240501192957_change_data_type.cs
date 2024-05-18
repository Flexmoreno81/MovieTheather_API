using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Theater_Model.Migrations
{
    /// <inheritdoc />
    public partial class change_data_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Screen_time",
                table: "ScreenTime",
                type: "VARCHAR(50)",
                maxLength: 50,
                precision: 2,
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(2)",
                oldPrecision: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Screen_time",
                table: "ScreenTime",
                type: "time(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldMaxLength: 50,
                oldPrecision: 2);
        }
    }
}
