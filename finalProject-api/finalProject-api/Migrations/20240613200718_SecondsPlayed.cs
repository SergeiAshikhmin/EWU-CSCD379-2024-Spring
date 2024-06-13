using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalProject_api.Migrations
{
    /// <inheritdoc />
    public partial class SecondsPlayed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "SecondsPlayed",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondsPlayed",
                table: "Games");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                table: "Games",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
