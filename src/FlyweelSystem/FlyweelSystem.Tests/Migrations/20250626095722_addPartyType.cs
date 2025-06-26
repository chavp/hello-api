using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyweelSystem.Tests.Migrations
{
    /// <inheritdoc />
    public partial class addPartyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartyId",
                schema: "flywheels",
                table: "Elements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartyTypeId",
                schema: "flywheels",
                table: "Elements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PartyTypes",
                schema: "flywheels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elements_PartyTypeId",
                schema: "flywheels",
                table: "Elements",
                column: "PartyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyTypes_Code",
                schema: "flywheels",
                table: "PartyTypes",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_PartyTypes_PartyTypeId",
                schema: "flywheels",
                table: "Elements",
                column: "PartyTypeId",
                principalSchema: "flywheels",
                principalTable: "PartyTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_PartyTypes_PartyTypeId",
                schema: "flywheels",
                table: "Elements");

            migrationBuilder.DropTable(
                name: "PartyTypes",
                schema: "flywheels");

            migrationBuilder.DropIndex(
                name: "IX_Elements_PartyTypeId",
                schema: "flywheels",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "PartyId",
                schema: "flywheels",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "PartyTypeId",
                schema: "flywheels",
                table: "Elements");
        }
    }
}
