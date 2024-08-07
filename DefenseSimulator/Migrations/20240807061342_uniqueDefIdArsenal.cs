using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class uniqueDefIdArsenal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Arsenal_DefenseWeaponId",
                table: "Arsenal");

            migrationBuilder.CreateIndex(
                name: "IX_Arsenal_DefenseWeaponId",
                table: "Arsenal",
                column: "DefenseWeaponId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Arsenal_DefenseWeaponId",
                table: "Arsenal");

            migrationBuilder.CreateIndex(
                name: "IX_Arsenal_DefenseWeaponId",
                table: "Arsenal",
                column: "DefenseWeaponId");
        }
    }
}
