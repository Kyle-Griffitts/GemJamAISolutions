using GemJamAISolutions.Data;
using GemJamAISolutions.Models;
using GemJamAISolutions.Services;
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

        // Get shelters sorted by distance from user location
        group.MapGet("/shelters/nearest", async (double lat, double lon, ApplicationDbContext context) =>
        {
            var shelters = await context.Shelters.ToListAsync();

            var sheltersWithDistance = shelters
                .Where(s => s.Latitude.HasValue && s.Longitude.HasValue)
                .Select(s =>
                {
                    var distance = DistanceCalculator.CalculateDistance(lat, lon, s.Latitude.Value, s.Longitude.Value, DistanceUnit.Miles);
                    return new ShelterWithDistance
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Address = s.Address,
                        City = s.City,
                        State = s.State,
                        ZipCode = s.ZipCode,
                        Latitude = s.Latitude,
                        Longitude = s.Longitude,
                        ShelterType = (int)s.ShelterType,
                        IsPetFriendly = s.IsPetFriendly,
                        OpenedDate = s.OpenedDate,
                        DistanceMiles = distance,
                        TravelTimeMinutes = DistanceCalculator.CalculateTravelTimeMinutes(distance)
                    };
                })
                .OrderBy(s => s.DistanceMiles)
                .ToList();

            return Results.Ok(sheltersWithDistance);
        });

        // Get sandbag distributions sorted by distance from user location
        group.MapGet("/sandbag-distributions/nearest", async (double lat, double lon, ApplicationDbContext context) =>
        {

            Console.WriteLine(lat);
            Console.WriteLine(lon);
            Console.WriteLine("Fetching sandbag distributions...");
            var distributions = await context.SandbagDistributions
                .Where(d => d.IsActive)
                .ToListAsync();

            var distributionsWithDistance = distributions
                .Where(d => d.Latitude.HasValue && d.Longitude.HasValue)
                .Select(d =>
                {
                    var distance = DistanceCalculator.CalculateDistance(lat, lon, d.Latitude.Value, d.Longitude.Value, DistanceUnit.Miles);
                    return new SandbagDistributionWithDistance
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Address = d.Address,
                        City = d.City,
                        State = d.State,
                        ZipCode = d.ZipCode,
                        Latitude = d.Latitude,
                        Longitude = d.Longitude,
                        MaxSandbagsPerResident = d.MaxSandbagsPerResident,
                        BringOwnShovel = d.BringOwnShovel,
                        AvailableFrom = d.AvailableFrom,
                        AvailableUntil = d.AvailableUntil,
                        IsActive = d.IsActive,
                        DistanceMiles = distance,
                        TravelTimeMinutes = DistanceCalculator.CalculateTravelTimeMinutes(distance)
                    };
                })
                .OrderBy(d => d.DistanceMiles)
                .ToList();

            return Results.Ok(distributionsWithDistance);
        });

        group.MapGet("/geocode", async (string street, string city, string state, IHttpClientFactory httpClientFactory) =>
        {
            try
            {
                // Add a small delay to respect Nominatim rate limits (1 req/sec)
                await Task.Delay(1000);
                Console.WriteLine($"Geocoding - Street: {street}, City: {city}, State: {state}");

                var httpClient = httpClientFactory.CreateClient();

                var geocodeUrl = $"https://nominatim.openstreetmap.org/search?street={Uri.EscapeDataString(street)}&city={Uri.EscapeDataString(city)}&state={Uri.EscapeDataString(state)}&country=United+States&format=json&limit=1";

                Console.WriteLine($"Geocode URL: {geocodeUrl}");

                var request = new HttpRequestMessage(HttpMethod.Get, geocodeUrl);
                request.Headers.Add("User-Agent", "FloodReadyAI/1.0 (Flood preparedness application)");

                var response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Response status: {response.StatusCode}");
                Console.WriteLine($"Response content: {content}");

                if (response.IsSuccessStatusCode && content != "[]")
                {
                    return Results.Content(content, "application/json");
                }

                // If structured query fails, try free-form search as fallback
                Console.WriteLine("Structured search returned no results, trying free-form search...");
                var fullAddress = $"{street}, {city}, {state}";
                var encodedAddress = Uri.EscapeDataString(fullAddress);
                var fallbackUrl = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1&countrycodes=us";

                await Task.Delay(1000); // Rate limit
                var fallbackRequest = new HttpRequestMessage(HttpMethod.Get, fallbackUrl);
                fallbackRequest.Headers.Add("User-Agent", "FloodReadyAI/1.0 (Flood preparedness application)");

                var fallbackResponse = await httpClient.SendAsync(fallbackRequest);
                var fallbackContent = await fallbackResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Fallback response: {fallbackContent}");

                if (fallbackResponse.IsSuccessStatusCode)
                {
                    return Results.Content(fallbackContent, "application/json");
                }

                return Results.Problem($"Geocoding service error: {response.StatusCode}", statusCode: 503);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Geocoding exception: {ex}");
                return Results.Problem($"Error geocoding address: {ex.Message}", statusCode: 500);
            }
        });
    }
}
