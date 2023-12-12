using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class relationShipBillAndTicketStop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BusStations_BusStationEndID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BusStations_BusStationStartID",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "BusStationStartID",
                table: "Bills",
                newName: "BusStationStartId");

            migrationBuilder.RenameColumn(
                name: "BusStationEndID",
                table: "Bills",
                newName: "BusStationEndId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_BusStationStartID",
                table: "Bills",
                newName: "IX_Bills_BusStationStartId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_BusStationEndID",
                table: "Bills",
                newName: "IX_Bills_BusStationEndId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Ticket_BusStop_BusStationEndId",
                table: "Bills",
                column: "BusStationEndId",
                principalTable: "Ticket_BusStop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Ticket_BusStop_BusStationStartId",
                table: "Bills",
                column: "BusStationStartId",
                principalTable: "Ticket_BusStop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Ticket_BusStop_BusStationEndId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Ticket_BusStop_BusStationStartId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "BusStationStartId",
                table: "Bills",
                newName: "BusStationStartID");

            migrationBuilder.RenameColumn(
                name: "BusStationEndId",
                table: "Bills",
                newName: "BusStationEndID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_BusStationStartId",
                table: "Bills",
                newName: "IX_Bills_BusStationStartID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_BusStationEndId",
                table: "Bills",
                newName: "IX_Bills_BusStationEndID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BusStations_BusStationEndID",
                table: "Bills",
                column: "BusStationEndID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BusStations_BusStationStartID",
                table: "Bills",
                column: "BusStationStartID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
