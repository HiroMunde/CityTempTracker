using CityTempTracker.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityTempTracker.Server.Controllers.CityTempTracker.Server.Controllers;

namespace CityTempTracker.Tests
{
    public class CityControllerTests
    {
        private async Task<AppDbContext> GetDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"CityTestDb_{Guid.NewGuid()}")
                .Options;

            var context = new AppDbContext(options);
            context.Cities.AddRange(
                new City { Id = 1, Name = "Stockholm", Country = "SE" },
                new City { Id = 2, Name = "Paris", Country = "FR" }
            );
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetCities_ReturnsAllCities()
        {
            var dbContext = await GetDbContextAsync();
            var controller = new CityController(dbContext);
            
            var result = controller.GetCities();
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var cities = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.Equal(2, cities.Count());
        }
    }
}
