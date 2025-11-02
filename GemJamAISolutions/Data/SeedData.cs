using Microsoft.EntityFrameworkCore;

namespace GemJamAISolutions.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        // Seed sample flood risk data if empty
        if (!await context.FloodRiskResults.AnyAsync())
        {
            var sampleData = new FloodRiskResult
            {
                Address = "123 Lake Eola Dr, Orlando, FL 32801",
                Lat = 28.5450f,
                Lon = -81.3730f,
                ElevationAtAddress = 105.5f,
                FloodZone = "Zone X (Minimal Risk)",
                IsInFloodZone = false,
                DemSource = "USGS 3DEP",
                ModelResponse = "Based on elevation analysis, this location is at minimal flood risk. " +
                              "The address elevation (105.5ft) is above the typical flood zone threshold. " +
                              "This area is designated as Zone X, indicating minimal flood hazard. " +
                              "However, residents should still maintain flood awareness and consider flood insurance.",
                CreatedAt = DateTime.UtcNow
            };

            context.FloodRiskResults.Add(sampleData);
            await context.SaveChangesAsync();
        }

        // Seed shelters if table is empty
        if (!await context.Shelters.AnyAsync())
        {
            var generalOpenedDate = new DateTime(2024, 10, 8, 18, 0, 0, DateTimeKind.Utc); // Oct 8, 2024 6PM
            var specialNeedsOpenedDate = new DateTime(2024, 10, 8, 10, 0, 0, DateTimeKind.Utc); // Oct 8, 2024 10AM

            var shelters = new List<Shelter>
            {
                // General Population Shelters
                new Shelter { Name = "Apopka High School", Address = "555 Martin St", City = "Apopka", ZipCode = "32712", Latitude = 28.6953, Longitude = -81.5116, ShelterType = ShelterType.General, IsPetFriendly = true, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Carver Middle School", Address = "4500 W. Columbia St", City = "Orlando", ZipCode = "32811", Latitude = 28.5650, Longitude = -81.4396, ShelterType = ShelterType.General, IsPetFriendly = true, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Colonial High School", Address = "6100 Oleander Dr", City = "Orlando", ZipCode = "32807", Latitude = 28.5540, Longitude = -81.2893, ShelterType = ShelterType.General, IsPetFriendly = true, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Lake Buena Vista High School", Address = "11305 Daryl Carter Pkwy", City = "Orlando", ZipCode = "32836", Latitude = 28.3872, Longitude = -81.5209, ShelterType = ShelterType.General, IsPetFriendly = false, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Lake Nona High School", Address = "12500 Narcoossee Rd", City = "Orlando", ZipCode = "32832", Latitude = 28.4349, Longitude = -81.2048, ShelterType = ShelterType.General, IsPetFriendly = true, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Oak Ridge High School", Address = "700 W. Oak Ridge Rd", City = "Orlando", ZipCode = "32809", Latitude = 28.4775, Longitude = -81.3925, ShelterType = ShelterType.General, IsPetFriendly = false, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Ocoee High School", Address = "1925 Ocoee Crown Point Pkwy", City = "Ocoee", ZipCode = "34761", Latitude = 28.5750, Longitude = -81.5430, ShelterType = ShelterType.General, IsPetFriendly = false, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Odyssey Middle School", Address = "9290 Lee Vista Blvd", City = "Orlando", ZipCode = "32829", Latitude = 28.4523, Longitude = -81.2733, ShelterType = ShelterType.General, IsPetFriendly = false, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Timber Springs Middle School", Address = "16001 Timber Park Ln", City = "Orlando", ZipCode = "32828", Latitude = 28.5613, Longitude = -81.1854, ShelterType = ShelterType.General, IsPetFriendly = false, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Union Park Middle School", Address = "1844 Westfall Dr", City = "Orlando", ZipCode = "32817", Latitude = 28.5673, Longitude = -81.2505, ShelterType = ShelterType.General, IsPetFriendly = true, OpenedDate = generalOpenedDate },
                new Shelter { Name = "Wedgefield K-8", Address = "3835 Bancroft Blvd", City = "Orlando", ZipCode = "32833", Latitude = 28.4608, Longitude = -80.9902, ShelterType = ShelterType.General, IsPetFriendly = false, OpenedDate = generalOpenedDate },

                // Special Needs Shelters (ALL Pet Friendly, opened at 10AM)
                new Shelter { Name = "Goldenrod Recreation Center", Address = "4863 N. Goldenrod Rd", City = "Winter Park", Latitude = 28.6331, Longitude = -81.2914, ShelterType = ShelterType.SpecialNeeds, IsPetFriendly = true, OpenedDate = specialNeedsOpenedDate },
                new Shelter { Name = "Silver Star Recreation Center", Address = "2801 N. Apopka-Vineland Rd", City = "Orlando", Latitude = 28.5675, Longitude = -81.5033, ShelterType = ShelterType.SpecialNeeds, IsPetFriendly = true, OpenedDate = specialNeedsOpenedDate },
                new Shelter { Name = "South Econ Park", Address = "3850 S. Econlockhatchee Trail", City = "Orlando", Latitude = 28.4738, Longitude = -81.2065, ShelterType = ShelterType.SpecialNeeds, IsPetFriendly = true, OpenedDate = specialNeedsOpenedDate }
            };

            context.Shelters.AddRange(shelters);
            await context.SaveChangesAsync();
        }

        // Seed sandbag distributions if table is empty
        if (!await context.SandbagDistributions.AnyAsync())
        {
            var availableFrom = new DateTime(2024, 10, 7, 9, 0, 0, DateTimeKind.Utc); // Oct 7, 2024 9AM
            var availableUntil = new DateTime(2024, 10, 8, 19, 0, 0, DateTimeKind.Utc); // Oct 8, 2024 7PM

            var sandbagDistributions = new List<SandbagDistribution>
            {
                new SandbagDistribution { Name = "Barnett Park", Address = "4801 W. Colonial Drive", City = "Orlando", ZipCode = "32808", Latitude = 28.5569, Longitude = -81.4391, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "Bithlo Community Park", Address = "18501 Washington Ave", City = "Orlando", ZipCode = "32820", Latitude = 28.5596, Longitude = -81.1046, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "Clarcona Horse Park", Address = "3535 Damon Road", City = "Apopka", ZipCode = "32703", Latitude = 28.6458, Longitude = -81.5617, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "Downey Park", Address = "10107 Flowers Ave", City = "Orlando", ZipCode = "32825", Latitude = 28.4911, Longitude = -81.1871, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "Meadow Woods Recreation Center", Address = "1751 Rhode Island Woods Circle", City = "Orlando", ZipCode = "32824", Latitude = 28.3774, Longitude = -81.3514, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "West Orange Recreation Center", Address = "309 S. West Crown Point Road", City = "Winter Garden", ZipCode = "32787", Latitude = 28.5547, Longitude = -81.6004, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "Edwards Field (Apopka)", Address = "Corner of Forest and 1st Street (behind Kit Land Nelson Park)", City = "Apopka", ZipCode = "32703", Latitude = 28.6958, Longitude = -81.5067, AvailableFrom = availableFrom, AvailableUntil = availableUntil },
                new SandbagDistribution { Name = "Winter Garden Public Services Complex", Address = "880 W. Bay St", City = "Winter Garden", ZipCode = "32883", Latitude = 28.5641, Longitude = -81.5864, AvailableFrom = availableFrom, AvailableUntil = availableUntil }
            };

            context.SandbagDistributions.AddRange(sandbagDistributions);
            await context.SaveChangesAsync();
        }
    }
}
