using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class responseAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    ResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreatId = table.Column<int>(type: "int", nullable: false),
                    LaunchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterceptTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefenseWeaponId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_Response_DefenseWeapon_DefenseWeaponId",
                        column: x => x.DefenseWeaponId,
                        principalTable: "DefenseWeapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Response_Threat_ThreatId",
                        column: x => x.ThreatId,
                        principalTable: "Threat",
                        principalColumn: "ThreatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Response_DefenseWeaponId",
                table: "Response",
                column: "DefenseWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_ThreatId",
                table: "Response",
                column: "ThreatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Response");
        }
    }
}
