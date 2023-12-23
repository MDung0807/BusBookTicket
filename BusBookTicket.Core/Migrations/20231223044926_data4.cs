using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class data4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Ticket_BusStop_BusStationEndId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Ticket_BusStop_BusStationStartId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BusStationEndId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BusStationStartId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BusStationEndId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BusStationStartId",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusStationEndId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusStationStartId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BusStationEndId",
                table: "Bills",
                column: "BusStationEndId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BusStationStartId",
                table: "Bills",
                column: "BusStationStartId");

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
    }
}
