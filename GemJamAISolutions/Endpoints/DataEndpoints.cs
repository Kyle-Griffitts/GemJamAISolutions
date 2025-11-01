using GemJamAISolutions.Data;
using Microsoft.EntityFrameworkCore;

namespace GemJamAISolutions.Endpoints;

public static class DataEndpoints
{
    public static void MapDataEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/data");

        group.MapGet("/shelters", async (ApplicationDbContext context) =>
        {
            var shelters = await context.Shelters
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Results.Ok(shelters);
        });

        group.MapGet("/sandbag-distributions", async (ApplicationDbContext context) =>
        {
            var distributions = await context.SandbagDistributions
                .Where(d => d.IsActive)
                .OrderBy(d => d.Name)
                .ToListAsync();

            return Results.Ok(distributions);
        });

        group.MapGet("/flood-risk/latest", async (ApplicationDbContext context) =>
        {
            var latestFloodRisk = await context.FloodRiskResults
                .OrderByDescending(f => f.CreatedAt)
                .FirstOrDefaultAsync();

            if (latestFloodRisk == null)
            {
                return Results.NotFound(new { message = "No flood risk data found" });
            }

            return Results.Ok(latestFloodRisk);
        });
    }
}
