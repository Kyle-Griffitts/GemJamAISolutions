namespace GemJamAISolutions.Services;

public static class DistanceCalculator
{
    private const double EarthRadiusMiles = 3959.0;
    private const double EarthRadiusKilometers = 6371.0;

    /// <summary>
    /// Calculate the distance between two points using the Haversine formula
    /// </summary>
    /// <param name="lat1">Latitude of first point</param>
    /// <param name="lon1">Longitude of first point</param>
    /// <param name="lat2">Latitude of second point</param>
    /// <param name="lon2">Longitude of second point</param>
    /// <param name="unit">Unit of measurement (miles or km)</param>
    /// <returns>Distance in specified unit</returns>
    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, DistanceUnit unit = DistanceUnit.Miles)
    {
        // Convert degrees to radians
        var lat1Rad = DegreesToRadians(lat1);
        var lon1Rad = DegreesToRadians(lon1);
        var lat2Rad = DegreesToRadians(lat2);
        var lon2Rad = DegreesToRadians(lon2);

        // Haversine formula
        var dLat = lat2Rad - lat1Rad;
        var dLon = lon2Rad - lon1Rad;

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var radius = unit == DistanceUnit.Miles ? EarthRadiusMiles : EarthRadiusKilometers;
        return radius * c;
    }

    /// <summary>
    /// Calculate estimated travel time based on distance
    /// </summary>
    /// <param name="distanceMiles">Distance in miles</param>
    /// <param name="averageSpeedMph">Average speed in miles per hour (default: 25 mph for urban driving)</param>
    /// <returns>Travel time in minutes</returns>
    public static double CalculateTravelTimeMinutes(double distanceMiles, double averageSpeedMph = 25.0)
    {
        // Time = Distance / Speed (in hours), then convert to minutes
        var timeHours = distanceMiles / averageSpeedMph;
        return timeHours * 60.0;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}

public enum DistanceUnit
{
    Miles,
    Kilometers
}
