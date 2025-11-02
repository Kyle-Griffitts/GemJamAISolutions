namespace GemJamAISolutions.Data;

public enum ShelterType
{
    General,
    SpecialNeeds
}

public class Shelter
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = "Florida";
    public string? ZipCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public ShelterType ShelterType { get; set; }
    public bool IsPetFriendly { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
