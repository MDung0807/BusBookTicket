using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class newVer3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StopStations_Prices_PricesId",
                table: "StopStations");

            migrationBuilder.DropIndex(
                name: "IX_StopStations_PricesId",
                table: "StopStations");

            migrationBuilder.DropIndex(
                name: "IX_RouteDetails_RouteId",
                table: "RouteDetails");

            migrationBuilder.DropColumn(
                name: "PricesId",
                table: "StopStations");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_RouteId",
                table: "RouteDetails",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RouteDetails_RouteId",
                table: "RouteDetails");

            migrationBuilder.AddColumn<int>(
                name: "PricesId",
                table: "StopStations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StopStations_PricesId",
                table: "StopStations",
                column: "PricesId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_RouteId",
                table: "RouteDetails",
                column: "RouteId",
                unique: true,
                filter: "[RouteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StopStations_Prices_PricesId",
                table: "StopStations",
                column: "PricesId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
