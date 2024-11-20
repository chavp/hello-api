using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MooDeng.Api.Tests.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                name: "People",
                schema: "parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People",
                schema: "parties");

            migrationBuilder.DropTable(
                name: "Parties",
                schema: "parties");
        }
    }
}
