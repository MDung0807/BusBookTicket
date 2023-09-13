using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBookTicket.Migrations
{
    /// <inheritdoc />
    public partial class auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Buses_busID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_busID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "busID",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "rank",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "companyID",
                table: "Buses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.accountID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buses_companyID",
                table: "Buses",
                column: "companyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Companies_companyID",
                table: "Buses",
                column: "companyID",
                principalTable: "Companies",
                principalColumn: "companyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Companies_companyID",
                table: "Buses");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_Buses_companyID",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "rank",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "companyID",
                table: "Buses");

            migrationBuilder.AddColumn<int>(
                name: "busID",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_busID",
                table: "Companies",
                column: "busID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Buses_busID",
                table: "Companies",
                column: "busID",
                principalTable: "Buses",
                principalColumn: "busID");
        }
    }
}
