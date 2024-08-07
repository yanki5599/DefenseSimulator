using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class changedOriginToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "OriginThreat",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_OriginThreat_Origin",
                table: "OriginThreat",
                newName: "IX_OriginThreat_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OriginThreat",
                newName: "Origin");

            migrationBuilder.RenameIndex(
                name: "IX_OriginThreat_Name",
                table: "OriginThreat",
                newName: "IX_OriginThreat_Origin");
        }
    }
}
