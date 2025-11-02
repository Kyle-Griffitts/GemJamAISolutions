namespace GemJamAISolutions.Client.Models;

public class FloodRiskResult
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public float Lat { get; set; }
    public float Lon { get; set; }
    public float? ElevationAtAddress { get; set; }
    public string? FloodZone { get; set; }
    public bool? IsInFloodZone { get; set; }
    public string? DemSource { get; set; }
    public string? ModelResponse { get; set; }
    public DateTime CreatedAt { get; set; }
}
