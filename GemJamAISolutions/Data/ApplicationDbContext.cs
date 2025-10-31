using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GemJamAISolutions.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FloodRiskResult> FloodRiskResults { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override defaults if needed

        // Configure FloodRiskResult table
        builder.Entity<FloodRiskResult>(entity =>
        {
            entity.ToTable("flood_risk_results");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address").IsRequired();
            entity.Property(e => e.Lat).HasColumnName("lat").IsRequired();
            entity.Property(e => e.Lon).HasColumnName("lon").IsRequired();
            entity.Property(e => e.ElevationAtAddress).HasColumnName("elevation_at_address");
            entity.Property(e => e.NearestContourElevation).HasColumnName("nearest_contour_elevation");
            entity.Property(e => e.ElevationDifference).HasColumnName("elevation_difference");
            entity.Property(e => e.FloodZone).HasColumnName("flood_zone");
            entity.Property(e => e.IsInFloodZone).HasColumnName("is_in_flood_zone");
            entity.Property(e => e.ContourSource).HasColumnName("contour_source");
            entity.Property(e => e.GeojsonPath).HasColumnName("geojson_path");
            entity.Property(e => e.ProcessedPath).HasColumnName("processed_path");
            entity.Property(e => e.ModelResponse).HasColumnName("model_response");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
