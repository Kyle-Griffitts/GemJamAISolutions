namespace GemJamAISolutions.Data;

public class SandbagDistribution
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = "Florida";
    public string? ZipCode { get; set; }
    public int MaxSandbagsPerResident { get; set; } = 10;
    public bool BringOwnShovel { get; set; } = true;
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
