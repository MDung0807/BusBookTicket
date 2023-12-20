using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class newver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceClassificationId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StopStationId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriceClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceClassifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationStartId = table.Column<int>(type: "int", nullable: true),
                    StationEndId = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_BusStations_StationEndId",
                        column: x => x.StationEndId,
                        principalTable: "BusStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_BusStations_StationStartId",
                        column: x => x.StationStartId,
                        principalTable: "BusStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prices_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StopStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: true),
                    PricesId = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StopStations_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StopStations_Prices_PricesId",
                        column: x => x.PricesId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RouteDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IndexStation = table.Column<int>(type: "int", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddDay = table.Column<int>(type: "int", nullable: false),
                    DiscountPrice = table.Column<double>(type: "float", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    StationId = table.Column<int>(type: "int", nullable: true),
                    PriceClassificationId = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteDetails_BusStations_StationId",
                        column: x => x.StationId,
                        principalTable: "BusStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteDetails_PriceClassifications_PriceClassificationId",
                        column: x => x.PriceClassificationId,
                        principalTable: "PriceClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteDetails_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteDetails_StopStations_Id",
                        column: x => x.Id,
                        principalTable: "StopStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketRouteDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    RouteDetailId = table.Column<int>(name: "RouteDetail Id", type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRouteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRouteDetails_RouteDetails_RouteDetail Id",
                        column: x => x.RouteDetailId,
                        principalTable: "RouteDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketRouteDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PriceClassificationId",
                table: "Tickets",
                column: "PriceClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StopStationId",
                table: "Tickets",
                column: "StopStationId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceClassifications_CompanyId",
                table: "PriceClassifications",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CompanyId",
                table: "Prices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RouteId",
                table: "Prices",
                column: "RouteId",
                unique: true,
                filter: "[RouteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_CompanyId",
                table: "RouteDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_PriceClassificationId",
                table: "RouteDetails",
                column: "PriceClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_RouteId",
                table: "RouteDetails",
                column: "RouteId",
                unique: true,
                filter: "[RouteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetails_StationId",
                table: "RouteDetails",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StationEndId",
                table: "Routes",
                column: "StationEndId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StationStartId",
                table: "Routes",
                column: "StationStartId");

            migrationBuilder.CreateIndex(
                name: "IX_StopStations_BusId",
                table: "StopStations",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_StopStations_PricesId",
                table: "StopStations",
                column: "PricesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRouteDetails_RouteDetail Id",
                table: "TicketRouteDetails",
                column: "RouteDetail Id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRouteDetails_TicketId",
                table: "TicketRouteDetails",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PriceClassifications_PriceClassificationId",
                table: "Tickets",
                column: "PriceClassificationId",
                principalTable: "PriceClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_StopStations_StopStationId",
                table: "Tickets",
                column: "StopStationId",
                principalTable: "StopStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PriceClassifications_PriceClassificationId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_StopStations_StopStationId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketRouteDetails");

            migrationBuilder.DropTable(
                name: "RouteDetails");

            migrationBuilder.DropTable(
                name: "PriceClassifications");

            migrationBuilder.DropTable(
                name: "StopStations");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PriceClassificationId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_StopStationId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PriceClassificationId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "StopStationId",
                table: "Tickets");
        }
    }
}
