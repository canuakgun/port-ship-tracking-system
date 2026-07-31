using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortShipTrackingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ShipVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ShipVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShipVisits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ShipVisits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ShipVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ships",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ships",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ShipCrewAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ShipCrewAssignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShipCrewAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ShipCrewAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ShipCrewAssignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Ports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Ports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CrewMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CrewMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CrewMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CrewMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CrewMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cargoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cargoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cargoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cargoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Cargoes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ShipVisits");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShipVisits");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShipVisits");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ShipVisits");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ShipVisits");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ShipCrewAssignments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShipCrewAssignments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShipCrewAssignments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ShipCrewAssignments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ShipCrewAssignments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CrewMembers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CrewMembers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CrewMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CrewMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CrewMembers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Cargoes");
        }
    }
}
