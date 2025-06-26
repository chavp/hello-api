using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyweelSystem.Tests.Migrations
{
    /// <inheritdoc />
    public partial class initFlywheels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "flywheels");

            migrationBuilder.CreateTable(
                name: "Boundaries",
                schema: "flywheels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boundaries", x => x.Id);
                });

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
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElementTypes",
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
                    table.PrimaryKey("PK_ElementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                schema: "flywheels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Technical = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ContextTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoundaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Boundaries_BoundaryId",
                        column: x => x.BoundaryId,
                        principalSchema: "flywheels",
                        principalTable: "Boundaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Elements_ElementTypes_ContextTypeId",
                        column: x => x.ContextTypeId,
                        principalSchema: "flywheels",
                        principalTable: "ElementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementRelationships",
                schema: "flywheels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ElementRelationshipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Technical = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementRelationships_Elements_FromElementId",
                        column: x => x.FromElementId,
                        principalSchema: "flywheels",
                        principalTable: "Elements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElementRelationships_Elements_ToElementId",
                        column: x => x.ToElementId,
                        principalSchema: "flywheels",
                        principalTable: "Elements",
                        principalColumn: "Id");
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
                unique: true,
                filter: "[FromElementId] IS NOT NULL AND [ToElementId] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_Elements_BoundaryId_Alias_ContextTypeId",
                schema: "flywheels",
                table: "Elements",
                columns: new[] { "BoundaryId", "Alias", "ContextTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ContextTypeId",
                schema: "flywheels",
                table: "Elements",
                column: "ContextTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementTypes_Code",
                schema: "flywheels",
                table: "ElementTypes",
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

            migrationBuilder.DropTable(
                name: "Elements",
                schema: "flywheels");

            migrationBuilder.DropTable(
                name: "Boundaries",
                schema: "flywheels");

            migrationBuilder.DropTable(
                name: "ElementTypes",
                schema: "flywheels");
        }
    }
}
