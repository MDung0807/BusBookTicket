using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class new_cons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prices_RouteId",
                table: "Prices");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RouteId_CompanyId",
                table: "Prices",
                columns: new[] { "RouteId", "CompanyId" },
                unique: true,
                filter: "[RouteId] IS NOT NULL AND [CompanyId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prices_RouteId_CompanyId",
                table: "Prices");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RouteId",
                table: "Prices",
                column: "RouteId");
        }
    }
}
