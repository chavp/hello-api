using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MooDeng.Parties.Mappings.Migrations
{
    /// <inheritdoc />
    public partial class addFacilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityRoleTypes",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FacilityTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalSchema: "parties",
                        principalTable: "FacilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityRoles",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityRoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EffectiveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityRoles_Facilities_OfFacilityId",
                        column: x => x.OfFacilityId,
                        principalSchema: "parties",
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityRoles_FacilityRoleTypes_FacilityRoleTypeId",
                        column: x => x.FacilityRoleTypeId,
                        principalSchema: "parties",
                        principalTable: "FacilityRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityRoles_Parties_ForPartyId",
                        column: x => x.ForPartyId,
                        principalSchema: "parties",
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lands",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lands_Facilities_Id",
                        column: x => x.Id,
                        principalSchema: "parties",
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_Code_FacilityTypeId",
                schema: "parties",
                table: "Facilities",
                columns: new[] { "Code", "FacilityTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityTypeId",
                schema: "parties",
                table: "Facilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityRoles_FacilityRoleTypeId",
                schema: "parties",
                table: "FacilityRoles",
                column: "FacilityRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityRoles_ForPartyId",
                schema: "parties",
                table: "FacilityRoles",
                column: "ForPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityRoles_OfFacilityId",
                schema: "parties",
                table: "FacilityRoles",
                column: "OfFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityRoleTypes_Code",
                schema: "parties",
                table: "FacilityRoleTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTypes_Code",
                schema: "parties",
                table: "FacilityTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityRoles",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Lands",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "FacilityRoleTypes",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Facilities",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "FacilityTypes",
                schema: "parties");
        }
    }
}
