using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class effrange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EffectiveDistance",
                table: "DefenseWeapon",
                newName: "Range");

            migrationBuilder.RenameColumn(
                name: "EffectiveDistance",
                table: "AttackWeapon",
                newName: "Range");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Range",
                table: "DefenseWeapon",
                newName: "EffectiveDistance");

            migrationBuilder.RenameColumn(
                name: "Range",
                table: "AttackWeapon",
                newName: "EffectiveDistance");
        }
    }
}
