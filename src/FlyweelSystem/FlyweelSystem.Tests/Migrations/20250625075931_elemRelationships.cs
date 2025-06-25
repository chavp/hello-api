using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyweelSystem.Tests.Migrations
{
    /// <inheritdoc />
    public partial class elemRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElementRelationshipTypes",
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
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElementRelationships",
                schema: "flywheels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElementRelationshipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementRelationships_ElementRelationshipTypes_ElementRelationshipTypeId",
                        column: x => x.ElementRelationshipTypeId,
                        principalSchema: "flywheels",
                        principalTable: "ElementRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ElementRelationships_Elements_FromElementId",
                        column: x => x.FromElementId,
                        principalSchema: "flywheels",
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ElementRelationships_Elements_ToElementId",
                        column: x => x.ToElementId,
                        principalSchema: "flywheels",
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementRelationships_ElementRelationshipTypeId",
                schema: "flywheels",
                table: "ElementRelationships",
                column: "ElementRelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementRelationships_FromElementId_ElementRelationshipTypeId_ToElementId",
                schema: "flywheels",
                table: "ElementRelationships",
                columns: new[] { "FromElementId", "ElementRelationshipTypeId", "ToElementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementRelationships_ToElementId",
                schema: "flywheels",
                table: "ElementRelationships",
                column: "ToElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementRelationshipTypes_Code",
                schema: "flywheels",
                table: "ElementRelationshipTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementRelationships",
                schema: "flywheels");

            migrationBuilder.DropTable(
                name: "ElementRelationshipTypes",
                schema: "flywheels");
        }
    }
}
