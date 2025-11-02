namespace GemJamAISolutions.Client.Models;

public class ShelterWithDistance
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int ShelterType { get; set; }
    public bool IsPetFriendly { get; set; }
    public DateTime? OpenedDate { get; set; }
    public double? DistanceMiles { get; set; }
    public double? TravelTimeMinutes { get; set; }
}

public class SandbagDistributionWithDistance
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int MaxSandbagsPerResident { get; set; }
    public bool BringOwnShovel { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public bool IsActive { get; set; }
    public double? DistanceMiles { get; set; }
    public double? TravelTimeMinutes { get; set; }
}
