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
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<SandbagDistribution> SandbagDistributions { get; set; }

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
            entity.Property(e => e.FloodZone).HasColumnName("flood_zone");
            entity.Property(e => e.IsInFloodZone).HasColumnName("is_in_flood_zone");
            entity.Property(e => e.DemSource).HasColumnName("dem_source");
            entity.Property(e => e.ModelResponse).HasColumnName("model_response");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Configure Shelter table
        builder.Entity<Shelter>(entity =>
        {
            entity.ToTable("shelters");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            entity.Property(e => e.Address).HasColumnName("address").IsRequired();
            entity.Property(e => e.City).HasColumnName("city").IsRequired();
            entity.Property(e => e.State).HasColumnName("state").IsRequired();
            entity.Property(e => e.ZipCode).HasColumnName("zip_code");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.ShelterType).HasColumnName("shelter_type").IsRequired();
            entity.Property(e => e.IsPetFriendly).HasColumnName("is_pet_friendly").IsRequired();
            entity.Property(e => e.OpenedDate).HasColumnName("opened_date");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Configure SandbagDistribution table
        builder.Entity<SandbagDistribution>(entity =>
        {
            entity.ToTable("sandbag_distributions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            entity.Property(e => e.Address).HasColumnName("address").IsRequired();
            entity.Property(e => e.City).HasColumnName("city").IsRequired();
            entity.Property(e => e.State).HasColumnName("state").IsRequired();
            entity.Property(e => e.ZipCode).HasColumnName("zip_code");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.MaxSandbagsPerResident).HasColumnName("max_sandbags_per_resident").IsRequired();
            entity.Property(e => e.BringOwnShovel).HasColumnName("bring_own_shovel").IsRequired();
            entity.Property(e => e.AvailableFrom).HasColumnName("available_from");
            entity.Property(e => e.AvailableUntil).HasColumnName("available_until");
            entity.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
