using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Common.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusesType",
                columns: table => new
                {
                    busTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusesType", x => x.busTypeID);
                });

            migrationBuilder.CreateTable(
                name: "BusStations",
                columns: table => new
                {
                    busStationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStations", x => x.busStationID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    companyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.companyID);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    rankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.rankID);
                });

            migrationBuilder.CreateTable(
                name: "SeatItems",
                columns: table => new
                {
                    seatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatNumber = table.Column<int>(type: "int", nullable: false),
                    busTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatItems", x => x.seatID);
                    table.ForeignKey(
                        name: "FK_SeatItems_BusesType_busTypeID",
                        column: x => x.busTypeID,
                        principalTable: "BusesType",
                        principalColumn: "busTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    busID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false),
                    busTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.busID);
                    table.ForeignKey(
                        name: "FK_Buses_BusesType_busTypeID",
                        column: x => x.busTypeID,
                        principalTable: "BusesType",
                        principalColumn: "busTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buses_Companies_companyID",
                        column: x => x.companyID,
                        principalTable: "Companies",
                        principalColumn: "companyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dateCreate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    dateUpdate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    rankID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Ranks_rankID",
                        column: x => x.rankID,
                        principalTable: "Ranks",
                        principalColumn: "rankID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    discountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    dateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rankID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.discountID);
                    table.ForeignKey(
                        name: "FK_Discounts_Ranks_rankID",
                        column: x => x.rankID,
                        principalTable: "Ranks",
                        principalColumn: "rankID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    busStopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busStationID = table.Column<int>(type: "int", nullable: false),
                    busID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.busStopID);
                    table.ForeignKey(
                        name: "FK_BusStops_BusStations_busStationID",
                        column: x => x.busStationID,
                        principalTable: "BusStations",
                        principalColumn: "busStationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusStops_Buses_busID",
                        column: x => x.busID,
                        principalTable: "Buses",
                        principalColumn: "busID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.accountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Companies_accountID",
                        column: x => x.accountID,
                        principalTable: "Companies",
                        principalColumn: "companyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_accountID",
                        column: x => x.accountID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reviews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rate = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Companies_companyID",
                        column: x => x.companyID,
                        principalTable: "Companies",
                        principalColumn: "companyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totolPrice = table.Column<long>(type: "bigint", nullable: false),
                    busStationEndID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    discountID = table.Column<int>(type: "int", nullable: false),
                    busStationStartID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticketID);
                    table.ForeignKey(
                        name: "FK_Tickets_BusStations_busStationEndID",
                        column: x => x.busStationEndID,
                        principalTable: "BusStations",
                        principalColumn: "busStationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Discounts_discountID",
                        column: x => x.discountID,
                        principalTable: "Discounts",
                        principalColumn: "discountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketItems",
                columns: table => new
                {
                    ticketItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketID = table.Column<int>(type: "int", nullable: false),
                    seatID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketItems", x => x.ticketItemID);
                    table.ForeignKey(
                        name: "FK_TicketItems_SeatItems_seatID",
                        column: x => x.seatID,
                        principalTable: "SeatItems",
                        principalColumn: "seatID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketItems_Tickets_ticketID",
                        column: x => x.ticketID,
                        principalTable: "Tickets",
                        principalColumn: "ticketID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buses_busTypeID",
                table: "Buses",
                column: "busTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_companyID",
                table: "Buses",
                column: "companyID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_busID",
                table: "BusStops",
                column: "busID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_busStationID",
                table: "BusStops",
                column: "busStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_rankID",
                table: "Customers",
                column: "rankID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_rankID",
                table: "Discounts",
                column: "rankID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_companyID",
                table: "Reviews",
                column: "companyID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_customerID",
                table: "Reviews",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatItems_busTypeID",
                table: "SeatItems",
                column: "busTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketItems_seatID",
                table: "TicketItems",
                column: "seatID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketItems_ticketID",
                table: "TicketItems",
                column: "ticketID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_busStationEndID",
                table: "Tickets",
                column: "busStationEndID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_customerID",
                table: "Tickets",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_discountID",
                table: "Tickets",
                column: "discountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TicketItems");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "SeatItems");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "BusesType");

            migrationBuilder.DropTable(
                name: "BusStations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Ranks");
        }
    }
}
