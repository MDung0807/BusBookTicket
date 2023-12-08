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
            migrationBuilder.RenameColumn(
                name: "objectModel",
                table: "Images",
                newName: "ObjectModel");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Images",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "id01",
                table: "Images",
                newName: "Id01");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObjectModel",
                table: "Images",
                newName: "objectModel");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Images",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Id01",
                table: "Images",
                newName: "id01");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
