using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyweelSystem.Tests.Migrations
{
    /// <inheritdoc />
    public partial class indexCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Elements_Code",
                schema: "flywheels",
                table: "Elements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Elements_Code",
                schema: "flywheels",
                table: "Elements",
                column: "Code",
                unique: true);
        }
    }
}
