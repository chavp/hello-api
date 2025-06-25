using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyweelSystem.Tests.Migrations
{
    /// <inheritdoc />
    public partial class elemRelationshipDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "flywheels",
                table: "ElementRelationships");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "flywheels",
                table: "ElementRelationships");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "flywheels",
                table: "ElementRelationships");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "flywheels",
                table: "ElementRelationships");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "flywheels",
                table: "ElementRelationships",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "flywheels",
                table: "ElementRelationships",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "flywheels",
                table: "ElementRelationships",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "flywheels",
                table: "ElementRelationships",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
