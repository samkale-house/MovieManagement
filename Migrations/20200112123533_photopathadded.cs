using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieManagement.Migrations
{
    public partial class photopathadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Movies",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Movies");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2020, 1, 11, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
