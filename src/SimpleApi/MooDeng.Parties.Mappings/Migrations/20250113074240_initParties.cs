using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MooDeng.Parties.Mappings.Migrations
{
    /// <inheritdoc />
    public partial class initParties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "parties");

            migrationBuilder.CreateTable(
                name: "Parties",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleTypes",
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
                    table.PrimaryKey("PK_PartyRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipPartyRoleTypes",
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
                    table.PrimaryKey("PK_RelationshipPartyRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Parties_Id",
                        column: x => x.Id,
                        principalSchema: "parties",
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Parties_Id",
                        column: x => x.Id,
                        principalSchema: "parties",
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Parties_Id",
                        column: x => x.Id,
                        principalSchema: "parties",
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoles",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyRoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_PartyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyRoles_Parties_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "parties",
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyRoles_PartyRoleTypes_PartyRoleTypeId",
                        column: x => x.PartyRoleTypeId,
                        principalSchema: "parties",
                        principalTable: "PartyRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipPartyRoles",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromPartyRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToPartyRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RelationshipPartyRoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_RelationshipPartyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipPartyRoles_PartyRoles_FromPartyRoleId",
                        column: x => x.FromPartyRoleId,
                        principalSchema: "parties",
                        principalTable: "PartyRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelationshipPartyRoles_PartyRoles_ToPartyRoleId",
                        column: x => x.ToPartyRoleId,
                        principalSchema: "parties",
                        principalTable: "PartyRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelationshipPartyRoles_RelationshipPartyRoleTypes_RelationshipPartyRoleTypeId",
                        column: x => x.RelationshipPartyRoleTypeId,
                        principalSchema: "parties",
                        principalTable: "RelationshipPartyRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Code",
                schema: "parties",
                table: "Organizations",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoles_PartyId",
                schema: "parties",
                table: "PartyRoles",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoles_PartyRoleTypeId",
                schema: "parties",
                table: "PartyRoles",
                column: "PartyRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleTypes_Code",
                schema: "parties",
                table: "PartyRoleTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipPartyRoles_FromPartyRoleId",
                schema: "parties",
                table: "RelationshipPartyRoles",
                column: "FromPartyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipPartyRoles_RelationshipPartyRoleTypeId",
                schema: "parties",
                table: "RelationshipPartyRoles",
                column: "RelationshipPartyRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipPartyRoles_ToPartyRoleId",
                schema: "parties",
                table: "RelationshipPartyRoles",
                column: "ToPartyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipPartyRoleTypes_Code",
                schema: "parties",
                table: "RelationshipPartyRoleTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "People",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "RelationshipPartyRoles",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "PartyRoles",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "RelationshipPartyRoleTypes",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Parties",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "PartyRoleTypes",
                schema: "parties");
        }
    }
}
