using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Core.Migrations
{
    /// <inheritdoc />
    public partial class dataNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_roleID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_billID",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_TicketItems_ticketItemID",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BusStations_busStationEndID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BusStations_busStationStartID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_customerID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Discounts_discountID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusesType_busTypeID",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Companies_companyID",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStations_Wards_wardId",
                table: "BusStations");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStops_BusStations_busStationID",
                table: "BusStops");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStops_Buses_busID",
                table: "BusStops");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Accounts_accountID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Wards_wardId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_accountID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Ranks_rankID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Ranks_rankID",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_AdministrativeUnits_administrativeUnitId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_provinceId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeRegions_administrativeRegionId",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeUnits_administrativeUnitId",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Buses_busID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_customerID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Buses_busID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_seatTypeID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatTypes_Companies_companyID",
                table: "SeatTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketItems_Tickets_ticketID",
                table: "TicketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Buses_busID",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_AdministrativeUnits_administrativeUnitId",
                table: "Wards");

            migrationBuilder.RenameColumn(
                name: "administrativeUnitId",
                table: "Wards",
                newName: "AdministrativeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Wards_administrativeUnitId",
                table: "Wards",
                newName: "IX_Wards_AdministrativeUnitId");

            migrationBuilder.RenameColumn(
                name: "busID",
                table: "Tickets",
                newName: "BusID");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_busID",
                table: "Tickets",
                newName: "IX_Tickets_BusID");

            migrationBuilder.RenameColumn(
                name: "ticketID",
                table: "TicketItems",
                newName: "TicketID");

            migrationBuilder.RenameIndex(
                name: "IX_TicketItems_ticketID",
                table: "TicketItems",
                newName: "IX_TicketItems_TicketID");

            migrationBuilder.RenameColumn(
                name: "companyID",
                table: "SeatTypes",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_SeatTypes_companyID",
                table: "SeatTypes",
                newName: "IX_SeatTypes_CompanyID");

            migrationBuilder.RenameColumn(
                name: "seatTypeID",
                table: "Seats",
                newName: "SeatTypeID");

            migrationBuilder.RenameColumn(
                name: "busID",
                table: "Seats",
                newName: "BusID");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_seatTypeID",
                table: "Seats",
                newName: "IX_Seats_SeatTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_busID",
                table: "Seats",
                newName: "IX_Seats_BusID");

            migrationBuilder.RenameColumn(
                name: "customerID",
                table: "Reviews",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "busID",
                table: "Reviews",
                newName: "BusID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_customerID",
                table: "Reviews",
                newName: "IX_Reviews_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_busID",
                table: "Reviews",
                newName: "IX_Reviews_BusID");

            migrationBuilder.RenameColumn(
                name: "administrativeUnitId",
                table: "Provinces",
                newName: "AdministrativeUnitId");

            migrationBuilder.RenameColumn(
                name: "administrativeRegionId",
                table: "Provinces",
                newName: "AdministrativeRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_administrativeUnitId",
                table: "Provinces",
                newName: "IX_Provinces_AdministrativeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_administrativeRegionId",
                table: "Provinces",
                newName: "IX_Provinces_AdministrativeRegionId");

            migrationBuilder.RenameColumn(
                name: "provinceId",
                table: "Districts",
                newName: "ProvinceId");

            migrationBuilder.RenameColumn(
                name: "administrativeUnitId",
                table: "Districts",
                newName: "AdministrativeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_provinceId",
                table: "Districts",
                newName: "IX_Districts_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_administrativeUnitId",
                table: "Districts",
                newName: "IX_Districts_AdministrativeUnitId");

            migrationBuilder.RenameColumn(
                name: "rankID",
                table: "Discounts",
                newName: "RankID");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_rankID",
                table: "Discounts",
                newName: "IX_Discounts_RankID");

            migrationBuilder.RenameColumn(
                name: "rankID",
                table: "Customers",
                newName: "RankID");

            migrationBuilder.RenameColumn(
                name: "accountID",
                table: "Customers",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_rankID",
                table: "Customers",
                newName: "IX_Customers_RankID");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_accountID",
                table: "Customers",
                newName: "IX_Customers_AccountID");

            migrationBuilder.RenameColumn(
                name: "wardId",
                table: "Companies",
                newName: "WardId");

            migrationBuilder.RenameColumn(
                name: "accountID",
                table: "Companies",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_wardId",
                table: "Companies",
                newName: "IX_Companies_WardId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_accountID",
                table: "Companies",
                newName: "IX_Companies_AccountID");

            migrationBuilder.RenameColumn(
                name: "busStationID",
                table: "BusStops",
                newName: "BusStationID");

            migrationBuilder.RenameColumn(
                name: "busID",
                table: "BusStops",
                newName: "BusID");

            migrationBuilder.RenameIndex(
                name: "IX_BusStops_busStationID",
                table: "BusStops",
                newName: "IX_BusStops_BusStationID");

            migrationBuilder.RenameIndex(
                name: "IX_BusStops_busID",
                table: "BusStops",
                newName: "IX_BusStops_BusID");

            migrationBuilder.RenameColumn(
                name: "wardId",
                table: "BusStations",
                newName: "WardId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStations_wardId",
                table: "BusStations",
                newName: "IX_BusStations_WardId");

            migrationBuilder.RenameColumn(
                name: "companyID",
                table: "Buses",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "busTypeID",
                table: "Buses",
                newName: "BusTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_companyID",
                table: "Buses",
                newName: "IX_Buses_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_busTypeID",
                table: "Buses",
                newName: "IX_Buses_BusTypeID");

            migrationBuilder.RenameColumn(
                name: "discountID",
                table: "Bills",
                newName: "DiscountID");

            migrationBuilder.RenameColumn(
                name: "customerID",
                table: "Bills",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "busStationStartID",
                table: "Bills",
                newName: "BusStationStartID");

            migrationBuilder.RenameColumn(
                name: "busStationEndID",
                table: "Bills",
                newName: "BusStationEndID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_discountID",
                table: "Bills",
                newName: "IX_Bills_DiscountID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_customerID",
                table: "Bills",
                newName: "IX_Bills_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_busStationStartID",
                table: "Bills",
                newName: "IX_Bills_BusStationStartID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_busStationEndID",
                table: "Bills",
                newName: "IX_Bills_BusStationEndID");

            migrationBuilder.RenameColumn(
                name: "ticketItemID",
                table: "BillItems",
                newName: "TicketItemID");

            migrationBuilder.RenameColumn(
                name: "billID",
                table: "BillItems",
                newName: "BillID");

            migrationBuilder.RenameIndex(
                name: "IX_BillItems_ticketItemID",
                table: "BillItems",
                newName: "IX_BillItems_TicketItemID");

            migrationBuilder.RenameIndex(
                name: "IX_BillItems_billID",
                table: "BillItems",
                newName: "IX_BillItems_BillID");

            migrationBuilder.RenameColumn(
                name: "roleID",
                table: "Accounts",
                newName: "RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_roleID",
                table: "Accounts",
                newName: "IX_Accounts_RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillID",
                table: "BillItems",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_TicketItems_TicketItemID",
                table: "BillItems",
                column: "TicketItemID",
                principalTable: "TicketItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BusStations_BusStationEndID",
                table: "Bills",
                column: "BusStationEndID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BusStations_BusStationStartID",
                table: "Bills",
                column: "BusStationStartID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerID",
                table: "Bills",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Discounts_DiscountID",
                table: "Bills",
                column: "DiscountID",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusesType_BusTypeID",
                table: "Buses",
                column: "BusTypeID",
                principalTable: "BusesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Companies_CompanyID",
                table: "Buses",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStations_Wards_WardId",
                table: "BusStations",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStops_BusStations_BusStationID",
                table: "BusStops",
                column: "BusStationID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStops_Buses_BusID",
                table: "BusStops",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Accounts_AccountID",
                table: "Companies",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Wards_WardId",
                table: "Companies",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_AccountID",
                table: "Customers",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Ranks_RankID",
                table: "Customers",
                column: "RankID",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Ranks_RankID",
                table: "Discounts",
                column: "RankID",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_AdministrativeUnits_AdministrativeUnitId",
                table: "Districts",
                column: "AdministrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeRegions_AdministrativeRegionId",
                table: "Provinces",
                column: "AdministrativeRegionId",
                principalTable: "AdministrativeRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeUnits_AdministrativeUnitId",
                table: "Provinces",
                column: "AdministrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Buses_BusID",
                table: "Reviews",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerID",
                table: "Reviews",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatTypes_SeatTypeID",
                table: "Seats",
                column: "SeatTypeID",
                principalTable: "SeatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatTypes_Companies_CompanyID",
                table: "SeatTypes",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketItems_Tickets_TicketID",
                table: "TicketItems",
                column: "TicketID",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Buses_BusID",
                table: "Tickets",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_AdministrativeUnits_AdministrativeUnitId",
                table: "Wards",
                column: "AdministrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillID",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_TicketItems_TicketItemID",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BusStations_BusStationEndID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BusStations_BusStationStartID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Discounts_DiscountID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusesType_BusTypeID",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Companies_CompanyID",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStations_Wards_WardId",
                table: "BusStations");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStops_BusStations_BusStationID",
                table: "BusStops");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStops_Buses_BusID",
                table: "BusStops");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Accounts_AccountID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Wards_WardId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_AccountID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Ranks_RankID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Ranks_RankID",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_AdministrativeUnits_AdministrativeUnitId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeRegions_AdministrativeRegionId",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_AdministrativeUnits_AdministrativeUnitId",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Buses_BusID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_SeatTypeID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatTypes_Companies_CompanyID",
                table: "SeatTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketItems_Tickets_TicketID",
                table: "TicketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Buses_BusID",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_AdministrativeUnits_AdministrativeUnitId",
                table: "Wards");

            migrationBuilder.RenameColumn(
                name: "AdministrativeUnitId",
                table: "Wards",
                newName: "administrativeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Wards_AdministrativeUnitId",
                table: "Wards",
                newName: "IX_Wards_administrativeUnitId");

            migrationBuilder.RenameColumn(
                name: "BusID",
                table: "Tickets",
                newName: "busID");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_BusID",
                table: "Tickets",
                newName: "IX_Tickets_busID");

            migrationBuilder.RenameColumn(
                name: "TicketID",
                table: "TicketItems",
                newName: "ticketID");

            migrationBuilder.RenameIndex(
                name: "IX_TicketItems_TicketID",
                table: "TicketItems",
                newName: "IX_TicketItems_ticketID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "SeatTypes",
                newName: "companyID");

            migrationBuilder.RenameIndex(
                name: "IX_SeatTypes_CompanyID",
                table: "SeatTypes",
                newName: "IX_SeatTypes_companyID");

            migrationBuilder.RenameColumn(
                name: "SeatTypeID",
                table: "Seats",
                newName: "seatTypeID");

            migrationBuilder.RenameColumn(
                name: "BusID",
                table: "Seats",
                newName: "busID");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_SeatTypeID",
                table: "Seats",
                newName: "IX_Seats_seatTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_BusID",
                table: "Seats",
                newName: "IX_Seats_busID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Reviews",
                newName: "customerID");

            migrationBuilder.RenameColumn(
                name: "BusID",
                table: "Reviews",
                newName: "busID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerID",
                table: "Reviews",
                newName: "IX_Reviews_customerID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_BusID",
                table: "Reviews",
                newName: "IX_Reviews_busID");

            migrationBuilder.RenameColumn(
                name: "AdministrativeUnitId",
                table: "Provinces",
                newName: "administrativeUnitId");

            migrationBuilder.RenameColumn(
                name: "AdministrativeRegionId",
                table: "Provinces",
                newName: "administrativeRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_AdministrativeUnitId",
                table: "Provinces",
                newName: "IX_Provinces_administrativeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_AdministrativeRegionId",
                table: "Provinces",
                newName: "IX_Provinces_administrativeRegionId");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "Districts",
                newName: "provinceId");

            migrationBuilder.RenameColumn(
                name: "AdministrativeUnitId",
                table: "Districts",
                newName: "administrativeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                newName: "IX_Districts_provinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_AdministrativeUnitId",
                table: "Districts",
                newName: "IX_Districts_administrativeUnitId");

            migrationBuilder.RenameColumn(
                name: "RankID",
                table: "Discounts",
                newName: "rankID");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_RankID",
                table: "Discounts",
                newName: "IX_Discounts_rankID");

            migrationBuilder.RenameColumn(
                name: "RankID",
                table: "Customers",
                newName: "rankID");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Customers",
                newName: "accountID");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_RankID",
                table: "Customers",
                newName: "IX_Customers_rankID");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AccountID",
                table: "Customers",
                newName: "IX_Customers_accountID");

            migrationBuilder.RenameColumn(
                name: "WardId",
                table: "Companies",
                newName: "wardId");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Companies",
                newName: "accountID");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_WardId",
                table: "Companies",
                newName: "IX_Companies_wardId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_AccountID",
                table: "Companies",
                newName: "IX_Companies_accountID");

            migrationBuilder.RenameColumn(
                name: "BusStationID",
                table: "BusStops",
                newName: "busStationID");

            migrationBuilder.RenameColumn(
                name: "BusID",
                table: "BusStops",
                newName: "busID");

            migrationBuilder.RenameIndex(
                name: "IX_BusStops_BusStationID",
                table: "BusStops",
                newName: "IX_BusStops_busStationID");

            migrationBuilder.RenameIndex(
                name: "IX_BusStops_BusID",
                table: "BusStops",
                newName: "IX_BusStops_busID");

            migrationBuilder.RenameColumn(
                name: "WardId",
                table: "BusStations",
                newName: "wardId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStations_WardId",
                table: "BusStations",
                newName: "IX_BusStations_wardId");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Buses",
                newName: "companyID");

            migrationBuilder.RenameColumn(
                name: "BusTypeID",
                table: "Buses",
                newName: "busTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_CompanyID",
                table: "Buses",
                newName: "IX_Buses_companyID");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_BusTypeID",
                table: "Buses",
                newName: "IX_Buses_busTypeID");

            migrationBuilder.RenameColumn(
                name: "DiscountID",
                table: "Bills",
                newName: "discountID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Bills",
                newName: "customerID");

            migrationBuilder.RenameColumn(
                name: "BusStationStartID",
                table: "Bills",
                newName: "busStationStartID");

            migrationBuilder.RenameColumn(
                name: "BusStationEndID",
                table: "Bills",
                newName: "busStationEndID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_DiscountID",
                table: "Bills",
                newName: "IX_Bills_discountID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_CustomerID",
                table: "Bills",
                newName: "IX_Bills_customerID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_BusStationStartID",
                table: "Bills",
                newName: "IX_Bills_busStationStartID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_BusStationEndID",
                table: "Bills",
                newName: "IX_Bills_busStationEndID");

            migrationBuilder.RenameColumn(
                name: "TicketItemID",
                table: "BillItems",
                newName: "ticketItemID");

            migrationBuilder.RenameColumn(
                name: "BillID",
                table: "BillItems",
                newName: "billID");

            migrationBuilder.RenameIndex(
                name: "IX_BillItems_TicketItemID",
                table: "BillItems",
                newName: "IX_BillItems_ticketItemID");

            migrationBuilder.RenameIndex(
                name: "IX_BillItems_BillID",
                table: "BillItems",
                newName: "IX_BillItems_billID");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "Accounts",
                newName: "roleID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                newName: "IX_Accounts_roleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_roleID",
                table: "Accounts",
                column: "roleID",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_billID",
                table: "BillItems",
                column: "billID",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_TicketItems_ticketItemID",
                table: "BillItems",
                column: "ticketItemID",
                principalTable: "TicketItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BusStations_busStationEndID",
                table: "Bills",
                column: "busStationEndID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BusStations_busStationStartID",
                table: "Bills",
                column: "busStationStartID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_customerID",
                table: "Bills",
                column: "customerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Discounts_discountID",
                table: "Bills",
                column: "discountID",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusesType_busTypeID",
                table: "Buses",
                column: "busTypeID",
                principalTable: "BusesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Companies_companyID",
                table: "Buses",
                column: "companyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStations_Wards_wardId",
                table: "BusStations",
                column: "wardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStops_BusStations_busStationID",
                table: "BusStops",
                column: "busStationID",
                principalTable: "BusStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStops_Buses_busID",
                table: "BusStops",
                column: "busID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Accounts_accountID",
                table: "Companies",
                column: "accountID",
                principalTable: "Accounts",
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
                name: "FK_Customers_Accounts_accountID",
                table: "Customers",
                column: "accountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Ranks_rankID",
                table: "Customers",
                column: "rankID",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Ranks_rankID",
                table: "Discounts",
                column: "rankID",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_AdministrativeUnits_administrativeUnitId",
                table: "Districts",
                column: "administrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_provinceId",
                table: "Districts",
                column: "provinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeRegions_administrativeRegionId",
                table: "Provinces",
                column: "administrativeRegionId",
                principalTable: "AdministrativeRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_AdministrativeUnits_administrativeUnitId",
                table: "Provinces",
                column: "administrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Buses_busID",
                table: "Reviews",
                column: "busID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_customerID",
                table: "Reviews",
                column: "customerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Buses_busID",
                table: "Seats",
                column: "busID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatTypes_seatTypeID",
                table: "Seats",
                column: "seatTypeID",
                principalTable: "SeatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatTypes_Companies_companyID",
                table: "SeatTypes",
                column: "companyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketItems_Tickets_ticketID",
                table: "TicketItems",
                column: "ticketID",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Buses_busID",
                table: "Tickets",
                column: "busID",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_AdministrativeUnits_administrativeUnitId",
                table: "Wards",
                column: "administrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
