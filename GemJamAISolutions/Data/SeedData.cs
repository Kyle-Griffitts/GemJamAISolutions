using Microsoft.EntityFrameworkCore;

namespace GemJamAISolutions.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        // Check if we already have data
        if (await context.FloodRiskResults.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Seed sample flood risk data for Orlando area
        var sampleData = new FloodRiskResult
        {
            Address = "123 Lake Eola Dr, Orlando, FL 32801",
            Lat = 28.5450f,
            Lon = -81.3730f,
            ElevationAtAddress = 105.5f,
            NearestContourElevation = 100.0f,
            ElevationDifference = 5.5f,
            FloodZone = "Zone X (Minimal Risk)",
            IsInFloodZone = false,
            ContourSource = "USGS Elevation Data",
            GeojsonPath = "/data/orlando_contours.geojson",
            ProcessedPath = "/data/processed/orlando_flood_analysis.json",
            ModelResponse = "Based on elevation analysis, this location is at minimal flood risk. " +
                          "The address elevation (105.5ft) is 5.5ft above the nearest flood contour (100ft). " +
                          "This area is designated as Zone X, indicating minimal flood hazard. " +
                          "However, residents should still maintain flood awareness and consider flood insurance.",
            CreatedAt = DateTime.UtcNow
        };

        context.FloodRiskResults.Add(sampleData);
        await context.SaveChangesAsync();
    }
}
