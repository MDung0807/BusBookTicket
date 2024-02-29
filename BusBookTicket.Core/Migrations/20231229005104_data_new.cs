using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class data_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteDetails_PriceClassifications_PriceClassificationId",
                table: "RouteDetails");

            migrationBuilder.DropIndex(
                name: "IX_RouteDetails_PriceClassificationId",
                table: "RouteDetails");

            migrationBuilder.DropIndex(
                name: "IX_Prices_RouteId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "PriceClassificationId",
                table: "RouteDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RouteId",
                table: "Prices",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prices_RouteId",
                table: "Prices");

            migrationBuilder.AddColumn<int>(
                name: "PriceClassificationId",
                table: "RouteDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_PriceClassificationId",
                table: "RouteDetails",
                column: "PriceClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RouteId",
                table: "Prices",
                column: "RouteId",
                unique: true,
                filter: "[RouteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteDetails_PriceClassifications_PriceClassificationId",
                table: "RouteDetails",
                column: "PriceClassificationId",
                principalTable: "PriceClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
