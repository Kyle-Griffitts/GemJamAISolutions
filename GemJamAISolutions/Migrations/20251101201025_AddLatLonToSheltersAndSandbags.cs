using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GemJamAISolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddLatLonToSheltersAndSandbags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "shelters",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "shelters",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "sandbag_distributions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "sandbag_distributions",
                type: "double precision",
                nullable: true);

            // Update existing shelter records with GPS coordinates
            migrationBuilder.Sql(@"
                UPDATE shelters SET latitude = 28.6953, longitude = -81.5116 WHERE name = 'Apopka High School';
                UPDATE shelters SET latitude = 28.5650, longitude = -81.4396 WHERE name = 'Carver Middle School';
                UPDATE shelters SET latitude = 28.5540, longitude = -81.2893 WHERE name = 'Colonial High School';
                UPDATE shelters SET latitude = 28.3872, longitude = -81.5209 WHERE name = 'Lake Buena Vista High School';
                UPDATE shelters SET latitude = 28.4349, longitude = -81.2048 WHERE name = 'Lake Nona High School';
                UPDATE shelters SET latitude = 28.4775, longitude = -81.3925 WHERE name = 'Oak Ridge High School';
                UPDATE shelters SET latitude = 28.5750, longitude = -81.5430 WHERE name = 'Ocoee High School';
                UPDATE shelters SET latitude = 28.4523, longitude = -81.2733 WHERE name = 'Odyssey Middle School';
                UPDATE shelters SET latitude = 28.5613, longitude = -81.1854 WHERE name = 'Timber Springs Middle School';
                UPDATE shelters SET latitude = 28.5673, longitude = -81.2505 WHERE name = 'Union Park Middle School';
                UPDATE shelters SET latitude = 28.4608, longitude = -80.9902 WHERE name = 'Wedgefield K-8';
                UPDATE shelters SET latitude = 28.6331, longitude = -81.2914 WHERE name = 'Goldenrod Recreation Center';
                UPDATE shelters SET latitude = 28.5675, longitude = -81.5033 WHERE name = 'Silver Star Recreation Center';
                UPDATE shelters SET latitude = 28.4738, longitude = -81.2065 WHERE name = 'South Econ Park';
            ");

            // Update existing sandbag distribution records with GPS coordinates
            migrationBuilder.Sql(@"
                UPDATE sandbag_distributions SET latitude = 28.5569, longitude = -81.4391 WHERE name = 'Barnett Park';
                UPDATE sandbag_distributions SET latitude = 28.5596, longitude = -81.1046 WHERE name = 'Bithlo Community Park';
                UPDATE sandbag_distributions SET latitude = 28.6458, longitude = -81.5617 WHERE name = 'Clarcona Horse Park';
                UPDATE sandbag_distributions SET latitude = 28.4911, longitude = -81.1871 WHERE name = 'Downey Park';
                UPDATE sandbag_distributions SET latitude = 28.3774, longitude = -81.3514 WHERE name = 'Meadow Woods Recreation Center';
                UPDATE sandbag_distributions SET latitude = 28.5547, longitude = -81.6004 WHERE name = 'West Orange Recreation Center';
                UPDATE sandbag_distributions SET latitude = 28.6958, longitude = -81.5067 WHERE name = 'Edwards Field (Apopka)';
                UPDATE sandbag_distributions SET latitude = 28.5641, longitude = -81.5864 WHERE name = 'Winter Garden Public Services Complex';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "shelters");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "shelters");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "sandbag_distributions");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "sandbag_distributions");
        }
    }
}
