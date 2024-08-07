using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class addedThreat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Threat",
                columns: table => new
                {
                    ThreatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginThreatId = table.Column<int>(type: "int", nullable: false),
                    LaunchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    AttackWeaponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threat", x => x.ThreatId);
                    table.ForeignKey(
                        name: "FK_Threat_AttackWeapon_AttackWeaponId",
                        column: x => x.AttackWeaponId,
                        principalTable: "AttackWeapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Threat_OriginThreat_OriginThreatId",
                        column: x => x.OriginThreatId,
                        principalTable: "OriginThreat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threat_AttackWeaponId",
                table: "Threat",
                column: "AttackWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Threat_OriginThreatId",
                table: "Threat",
                column: "OriginThreatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Threat");
        }
    }
}
