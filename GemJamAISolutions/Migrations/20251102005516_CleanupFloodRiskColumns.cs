using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GemJamAISolutions.Migrations
{
    /// <inheritdoc />
    public partial class CleanupFloodRiskColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contour_source",
                table: "flood_risk_results");

            migrationBuilder.DropColumn(
                name: "elevation_difference",
                table: "flood_risk_results");

            migrationBuilder.DropColumn(
                name: "geojson_path",
                table: "flood_risk_results");

            migrationBuilder.DropColumn(
                name: "nearest_contour_elevation",
                table: "flood_risk_results");

            migrationBuilder.RenameColumn(
                name: "processed_path",
                table: "flood_risk_results",
                newName: "dem_source");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dem_source",
                table: "flood_risk_results",
                newName: "processed_path");

            migrationBuilder.AddColumn<string>(
                name: "contour_source",
                table: "flood_risk_results",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "elevation_difference",
                table: "flood_risk_results",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "geojson_path",
                table: "flood_risk_results",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "nearest_contour_elevation",
                table: "flood_risk_results",
                type: "real",
                nullable: true);
        }
    }
}
