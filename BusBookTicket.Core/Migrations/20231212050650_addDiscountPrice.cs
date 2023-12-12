using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class addDiscountPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountPrice",
                table: "Ticket_BusStop",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Ticket_BusStop");
        }
    }
}
