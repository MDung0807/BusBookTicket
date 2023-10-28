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
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_SeatItems_ticketItemID",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatItems_Tickets_ticketID",
                table: "SeatItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatItems",
                table: "SeatItems");

            migrationBuilder.RenameTable(
                name: "SeatItems",
                newName: "TicketItems");

            migrationBuilder.RenameIndex(
                name: "IX_SeatItems_ticketID",
                table: "TicketItems",
                newName: "IX_TicketItems_ticketID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketItems",
                table: "TicketItems",
                column: "ticketItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_TicketItems_ticketItemID",
                table: "BillItems",
                column: "ticketItemID",
                principalTable: "TicketItems",
                principalColumn: "ticketItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketItems_Tickets_ticketID",
                table: "TicketItems",
                column: "ticketID",
                principalTable: "Tickets",
                principalColumn: "ticketID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_TicketItems_ticketItemID",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketItems_Tickets_ticketID",
                table: "TicketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketItems",
                table: "TicketItems");

            migrationBuilder.RenameTable(
                name: "TicketItems",
                newName: "SeatItems");

            migrationBuilder.RenameIndex(
                name: "IX_TicketItems_ticketID",
                table: "SeatItems",
                newName: "IX_SeatItems_ticketID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatItems",
                table: "SeatItems",
                column: "ticketItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_SeatItems_ticketItemID",
                table: "BillItems",
                column: "ticketItemID",
                principalTable: "SeatItems",
                principalColumn: "ticketItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatItems_Tickets_ticketID",
                table: "SeatItems",
                column: "ticketID",
                principalTable: "Tickets",
                principalColumn: "ticketID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
