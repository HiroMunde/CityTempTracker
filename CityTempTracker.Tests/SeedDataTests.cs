using CityTempTracker.Server.Data;
using CityTempTracker.Server.Data.CityTempTracker.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CityTempTracker.Tests
{
    public class SeedDataTests
    {
        private AppDbContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"SeedTestDb_{Guid.NewGuid()}")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void Seed_Adds_Cities_If_Empty()
        {
            var context = GetEmptyDbContext();
            
            SeedData.Initialize(context);
            
            var cities = context.Cities.ToList();
            Assert.Equal(3, cities.Count);
            Assert.Contains(cities, c => c.Name == "London");
            Assert.Contains(cities, c => c.Name == "Paris");
            Assert.Contains(cities, c => c.Name == "Stockholm");
        }

        [Fact]
        public void Seed_Does_Not_Add_If_Already_Seeded()
        {
            var context = GetEmptyDbContext();
            SeedData.Initialize(context);
            SeedData.Initialize(context);
            
            Assert.Equal(3, context.Cities.Count());
        }
    }
}
