using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BamBurrito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToOfferRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OfferRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "OfferRequests");
        }
    }
}
