using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class data3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRouteDetails_RouteDetails_RouteDetail Id",
                table: "TicketRouteDetails");

            migrationBuilder.RenameColumn(
                name: "RouteDetail Id",
                table: "TicketRouteDetails",
                newName: "RouteDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRouteDetails_RouteDetail Id",
                table: "TicketRouteDetails",
                newName: "IX_TicketRouteDetails_RouteDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRouteDetails_RouteDetails_RouteDetailId",
                table: "TicketRouteDetails",
                column: "RouteDetailId",
                principalTable: "RouteDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRouteDetails_RouteDetails_RouteDetailId",
                table: "TicketRouteDetails");

            migrationBuilder.RenameColumn(
                name: "RouteDetailId",
                table: "TicketRouteDetails",
                newName: "RouteDetail Id");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRouteDetails_RouteDetailId",
                table: "TicketRouteDetails",
                newName: "IX_TicketRouteDetails_RouteDetail Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRouteDetails_RouteDetails_RouteDetail Id",
                table: "TicketRouteDetails",
                column: "RouteDetail Id",
                principalTable: "RouteDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
