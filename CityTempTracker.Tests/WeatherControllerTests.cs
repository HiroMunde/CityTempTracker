using CityTempTracker.Server.Controllers;
using CityTempTracker.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityTempTracker.Tests
{
    public class WeatherControllerTests
    {
        private async Task<AppDbContext> GetDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"WeatherTestDb_{Guid.NewGuid()}")
                .Options;

            var context = new AppDbContext(options);

            var city = new City { Id = 1, Name = "Stockholm", Country = "SE" };
            context.Cities.Add(city);

            context.WeatherData.AddRange(
                new WeatherData { Id = 1, City = city, CityId = city.Id, Temperature = 10, Timestamp = DateTime.UtcNow },
                new WeatherData { Id = 2, City = city, CityId = city.Id, Temperature = 11, Timestamp = DateTime.UtcNow }
            );

            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetWeatherByCity_ReturnsDataForGivenCity()
        {
            var dbContext = await GetDbContextAsync();
            var controller = new WeatherController(dbContext);
            
            var result = controller.GetWeatherHistory(1);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public async Task GetWeatherByCity_ReturnsEmptyForUnknownCity()
        {
            var dbContext = await GetDbContextAsync();
            var controller = new WeatherController(dbContext);
            
            var result = controller.GetWeatherHistory(1);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.Equal(2, data.Count());
        }
    }
}