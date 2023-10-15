using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Common.Migrations
{
    /// <inheritdoc />
    public partial class dataNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_busStationStartID",
                table: "Tickets",
                column: "busStationStartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusStations_busStationStartID",
                table: "Tickets",
                column: "busStationStartID",
                principalTable: "BusStations",
                principalColumn: "busStationID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusStations_busStationStartID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_busStationStartID",
                table: "Tickets");
        }
    }
}
