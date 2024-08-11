using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefenseSimulator.Migrations
{
    /// <inheritdoc />
    public partial class added_treatFileds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveID",
                table: "Threat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterceptedOrExploded",
                table: "Threat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveID",
                table: "Threat");

            migrationBuilder.DropColumn(
                name: "IsInterceptedOrExploded",
                table: "Threat");
        }
    }
}
