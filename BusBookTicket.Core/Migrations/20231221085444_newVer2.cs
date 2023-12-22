using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class newVer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteDetails_StopStations_Id",
                table: "RouteDetails");

            migrationBuilder.DropIndex(
                name: "IX_PriceClassifications_CompanyId",
                table: "PriceClassifications");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "StopStations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StationStartId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StationEndId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);


            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Prices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Surcharges",
                table: "Prices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_StopStations_RouteId",
                table: "StopStations",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceClassifications_CompanyId",
                table: "PriceClassifications",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_StopStations_Routes_RouteId",
                table: "StopStations",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StopStations_Routes_RouteId",
                table: "StopStations");

            migrationBuilder.DropIndex(
                name: "IX_StopStations_RouteId",
                table: "StopStations");

            migrationBuilder.DropIndex(
                name: "IX_PriceClassifications_CompanyId",
                table: "PriceClassifications");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "StopStations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "Surcharges",
                table: "Prices");

            migrationBuilder.AlterColumn<int>(
                name: "StationStartId",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StationEndId",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RouteDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_PriceClassifications_CompanyId",
                table: "PriceClassifications",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteDetails_StopStations_Id",
                table: "RouteDetails",
                column: "Id",
                principalTable: "StopStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
