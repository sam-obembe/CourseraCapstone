using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingCosponsoredLegislation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CosponsoredLegislation",
                table: "CongressMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SponsoredLegislation",
                table: "CongressMember",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CosponsoredLegislation",
                table: "CongressMember");

            migrationBuilder.DropColumn(
                name: "SponsoredLegislation",
                table: "CongressMember");
        }
    }
}
