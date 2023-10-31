using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
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
                    totalSeats = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
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
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStations", x => x.busStationID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id01 = table.Column<int>(type: "int", nullable: false),
                    objectModel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    rankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.rankID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleID);
                    table.UniqueConstraint("AK_Roles_roleName", x => x.roleName);
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
                    status = table.Column<int>(type: "int", nullable: false),
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
                name: "Accounts",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    roleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.accountID);
                    table.UniqueConstraint("AK_Accounts_username", x => x.username);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_roleID",
                        column: x => x.roleID,
                        principalTable: "Roles",
                        principalColumn: "roleID",
                        onDelete: ReferentialAction.Restrict);
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
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    accountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.companyID);
                    table.ForeignKey(
                        name: "FK_Companies_Accounts_accountID",
                        column: x => x.accountID,
                        principalTable: "Accounts",
                        principalColumn: "accountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dateCreate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    dateUpdate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    accountID = table.Column<int>(type: "int", nullable: false),
                    rankID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerID);
                    table.ForeignKey(
                        name: "FK_Customers_Accounts_accountID",
                        column: x => x.accountID,
                        principalTable: "Accounts",
                        principalColumn: "accountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Ranks_rankID",
                        column: x => x.rankID,
                        principalTable: "Ranks",
                        principalColumn: "rankID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    busID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
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
                name: "SeatTypes",
                columns: table => new
                {
                    typeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pirce = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    companyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTypes", x => x.typeID);
                    table.ForeignKey(
                        name: "FK_SeatTypes_Companies_companyID",
                        column: x => x.companyID,
                        principalTable: "Companies",
                        principalColumn: "companyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    billID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totolPrice = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    busStationStartID = table.Column<int>(type: "int", nullable: false),
                    busStationEndID = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    discountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.billID);
                    table.ForeignKey(
                        name: "FK_Bills_BusStations_busStationEndID",
                        column: x => x.busStationEndID,
                        principalTable: "BusStations",
                        principalColumn: "busStationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_BusStations_busStationStartID",
                        column: x => x.busStationStartID,
                        principalTable: "BusStations",
                        principalColumn: "busStationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Discounts_discountID",
                        column: x => x.discountID,
                        principalTable: "Discounts",
                        principalColumn: "discountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    busStopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false),
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
                name: "Reviews",
                columns: table => new
                {
                    reviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reviews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    busID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Buses_busID",
                        column: x => x.busID,
                        principalTable: "Buses",
                        principalColumn: "busID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_customerID",
                        column: x => x.customerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    busID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticketID);
                    table.ForeignKey(
                        name: "FK_Tickets_Buses_busID",
                        column: x => x.busID,
                        principalTable: "Buses",
                        principalColumn: "busID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    seatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    busID = table.Column<int>(type: "int", nullable: false),
                    seatTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.seatID);
                    table.ForeignKey(
                        name: "FK_Seats_Buses_busID",
                        column: x => x.busID,
                        principalTable: "Buses",
                        principalColumn: "busID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seats_SeatTypes_seatTypeID",
                        column: x => x.seatTypeID,
                        principalTable: "SeatTypes",
                        principalColumn: "typeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketItems",
                columns: table => new
                {
                    ticketItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatNumber = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    ticketID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketItems", x => x.ticketItemID);
                    table.ForeignKey(
                        name: "FK_TicketItems_Tickets_ticketID",
                        column: x => x.ticketID,
                        principalTable: "Tickets",
                        principalColumn: "ticketID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    billItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billID = table.Column<int>(type: "int", nullable: false),
                    ticketItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.billItemID);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_billID",
                        column: x => x.billID,
                        principalTable: "Bills",
                        principalColumn: "billID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillItems_TicketItems_ticketItemID",
                        column: x => x.ticketItemID,
                        principalTable: "TicketItems",
                        principalColumn: "ticketItemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_roleID",
                table: "Accounts",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_billID",
                table: "BillItems",
                column: "billID");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_ticketItemID",
                table: "BillItems",
                column: "ticketItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_busStationEndID",
                table: "Bills",
                column: "busStationEndID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_busStationStartID",
                table: "Bills",
                column: "busStationStartID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_customerID",
                table: "Bills",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_discountID",
                table: "Bills",
                column: "discountID");

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
                name: "IX_Companies_accountID",
                table: "Companies",
                column: "accountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_accountID",
                table: "Customers",
                column: "accountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_customerID",
                table: "Customers",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_rankID",
                table: "Customers",
                column: "rankID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_rankID",
                table: "Discounts",
                column: "rankID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_busID",
                table: "Reviews",
                column: "busID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_customerID",
                table: "Reviews",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_busID",
                table: "Seats",
                column: "busID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_seatTypeID",
                table: "Seats",
                column: "seatTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatTypes_companyID",
                table: "SeatTypes",
                column: "companyID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketItems_ticketID",
                table: "TicketItems",
                column: "ticketID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_busID",
                table: "Tickets",
                column: "busID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "TicketItems");

            migrationBuilder.DropTable(
                name: "SeatTypes");

            migrationBuilder.DropTable(
                name: "BusStations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "BusesType");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
