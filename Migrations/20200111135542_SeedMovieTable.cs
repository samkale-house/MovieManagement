using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieManagement.Migrations
{
    public partial class SeedMovieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "ID", "Genre", "Price", "Rating", "ReleaseDate", "Title" },
                values: new object[] { 1, 0, 23m, 3, new DateTime(2020, 1, 11, 0, 0, 0, 0, DateTimeKind.Local), "Star Wars" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
