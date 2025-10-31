using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GemJamAISolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddFloodRiskResultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flood_risk_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "text", nullable: false),
                    lat = table.Column<float>(type: "real", nullable: false),
                    lon = table.Column<float>(type: "real", nullable: false),
                    elevation_at_address = table.Column<float>(type: "real", nullable: true),
                    nearest_contour_elevation = table.Column<float>(type: "real", nullable: true),
                    elevation_difference = table.Column<float>(type: "real", nullable: true),
                    flood_zone = table.Column<string>(type: "text", nullable: true),
                    is_in_flood_zone = table.Column<bool>(type: "boolean", nullable: true),
                    contour_source = table.Column<string>(type: "text", nullable: true),
                    geojson_path = table.Column<string>(type: "text", nullable: true),
                    processed_path = table.Column<string>(type: "text", nullable: true),
                    model_response = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flood_risk_results", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flood_risk_results");
        }
    }
}
