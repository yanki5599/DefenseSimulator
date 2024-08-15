using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class addedThreatStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveID",
                table: "Threat");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Threat");

            migrationBuilder.DropColumn(
                name: "IsInterceptedOrExploded",
                table: "Threat");

            migrationBuilder.AddColumn<int>(
                name: "ThreatStatus",
                table: "Threat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThreatStatus",
                table: "Threat");

            migrationBuilder.AddColumn<string>(
                name: "ActiveID",
                table: "Threat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Threat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterceptedOrExploded",
                table: "Threat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
