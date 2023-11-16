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
                name: "wardId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "wardId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "wardId",
                table: "BusStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_wardId",
                table: "Customers",
                column: "wardId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_wardId",
                table: "Companies",
                column: "wardId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStations_wardId",
                table: "BusStations",
                column: "wardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStations_Wards_wardId",
                table: "BusStations",
                column: "wardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Wards_wardId",
                table: "Companies",
                column: "wardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Wards_wardId",
                table: "Customers",
                column: "wardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusStations_Wards_wardId",
                table: "BusStations");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Wards_wardId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Wards_wardId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_wardId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_wardId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_BusStations_wardId",
                table: "BusStations");

            migrationBuilder.DropColumn(
                name: "wardId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "wardId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "wardId",
                table: "BusStations");
        }
    }
}
