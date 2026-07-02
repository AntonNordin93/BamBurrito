using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BamBurrito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class picupload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "LocationEvents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "LocationEvents");
        }
    }
}
