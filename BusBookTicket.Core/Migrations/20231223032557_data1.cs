using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class data1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketRouteDetailEndId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketRouteDetailStartId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TicketRouteDetailEndId",
                table: "Bills",
                column: "TicketRouteDetailEndId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TicketRouteDetailStartId",
                table: "Bills",
                column: "TicketRouteDetailStartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_TicketRouteDetails_TicketRouteDetailEndId",
                table: "Bills",
                column: "TicketRouteDetailEndId",
                principalTable: "TicketRouteDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_TicketRouteDetails_TicketRouteDetailStartId",
                table: "Bills",
                column: "TicketRouteDetailStartId",
                principalTable: "TicketRouteDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_TicketRouteDetails_TicketRouteDetailEndId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_TicketRouteDetails_TicketRouteDetailStartId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_TicketRouteDetailEndId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_TicketRouteDetailStartId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TicketRouteDetailEndId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TicketRouteDetailStartId",
                table: "Bills");
        }
    }
}
