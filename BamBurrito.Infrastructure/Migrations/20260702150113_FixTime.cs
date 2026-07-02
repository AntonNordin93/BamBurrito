using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BamBurrito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "LocationEvents",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "LocationEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "LocationEvents");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "LocationEvents",
                newName: "EventDate");
        }
    }
}
