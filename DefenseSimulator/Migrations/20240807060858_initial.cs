using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttackWeapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    EffectiveDistance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackWeapon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefenseWeapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    EffectiveDistance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenseWeapon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arsenal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DefenseWeaponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arsenal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arsenal_DefenseWeapon_DefenseWeaponId",
                        column: x => x.DefenseWeaponId,
                        principalTable: "DefenseWeapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttackWeaponDefenseWeapon",
                columns: table => new
                {
                    VulnerableWeaponsId = table.Column<int>(type: "int", nullable: false),
                    defenseWeaponsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackWeaponDefenseWeapon", x => new { x.VulnerableWeaponsId, x.defenseWeaponsId });
                    table.ForeignKey(
                        name: "FK_AttackWeaponDefenseWeapon_AttackWeapon_VulnerableWeaponsId",
                        column: x => x.VulnerableWeaponsId,
                        principalTable: "AttackWeapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttackWeaponDefenseWeapon_DefenseWeapon_defenseWeaponsId",
                        column: x => x.defenseWeaponsId,
                        principalTable: "DefenseWeapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arsenal_DefenseWeaponId",
                table: "Arsenal",
                column: "DefenseWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_AttackWeaponDefenseWeapon_defenseWeaponsId",
                table: "AttackWeaponDefenseWeapon",
                column: "defenseWeaponsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arsenal");

            migrationBuilder.DropTable(
                name: "AttackWeaponDefenseWeapon");

            migrationBuilder.DropTable(
                name: "AttackWeapon");

            migrationBuilder.DropTable(
                name: "DefenseWeapon");
        }
    }
}
