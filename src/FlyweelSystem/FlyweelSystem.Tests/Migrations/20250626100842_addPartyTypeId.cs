using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyweelSystem.Tests.Migrations
{
    /// <inheritdoc />
    public partial class addPartyTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyId",
                schema: "flywheels",
                table: "Elements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartyId",
                schema: "flywheels",
                table: "Elements",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
