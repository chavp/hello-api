using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MooDeng.Parties.Mappings.Migrations
{
    /// <inheritdoc />
    public partial class initMooDengDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "parties");

            migrationBuilder.CreateTable(
                name: "FacilityRoleTypes",
                schema: "parties",
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
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                name: "Parties",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                name: "RelationshipPartyTypes",
                schema: "parties",
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipPartyTypes", x => x.Id);
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
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                name: "FacilityRoles",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityRoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "RelationshipParties",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromPartyRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToPartyRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RelationshipPartyRoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Revision = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EffectiveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipParties_PartyRoles_FromPartyRoleId",
                        column: x => x.FromPartyRoleId,
                        principalSchema: "parties",
                        principalTable: "PartyRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelationshipParties_PartyRoles_ToPartyRoleId",
                        column: x => x.ToPartyRoleId,
                        principalSchema: "parties",
                        principalTable: "PartyRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelationshipParties_RelationshipPartyTypes_RelationshipPartyRoleTypeId",
                        column: x => x.RelationshipPartyRoleTypeId,
                        principalSchema: "parties",
                        principalTable: "RelationshipPartyTypes",
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
                name: "IX_RelationshipParties_FromPartyRoleId",
                schema: "parties",
                table: "RelationshipParties",
                column: "FromPartyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipParties_RelationshipPartyRoleTypeId",
                schema: "parties",
                table: "RelationshipParties",
                column: "RelationshipPartyRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipParties_ToPartyRoleId",
                schema: "parties",
                table: "RelationshipParties",
                column: "ToPartyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipPartyTypes_Code",
                schema: "parties",
                table: "RelationshipPartyTypes",
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
                name: "FacilityRoles",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Lands",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "People",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "RelationshipParties",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "FacilityRoleTypes",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Facilities",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "PartyRoles",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "RelationshipPartyTypes",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "FacilityTypes",
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
