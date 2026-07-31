using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortShipTrackingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToShipVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Ships",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "shipId",
                table: "Ships",
                newName: "ShipId");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ShipVisits",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ShipVisits");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ships",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ShipId",
                table: "Ships",
                newName: "shipId");
        }
    }
}
