using CityTempTracker.Server.Data;
using CityTempTracker.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CityTempTracker.Tests.Mocks;

namespace CityTempTracker.Tests;

public class WeatherServiceTests
{
    [Fact]
    public async Task UpdateWeatherAsync_SavesWeatherData_WhenApiReturnsValidResponse()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var db = new AppDbContext(options);
        db.Cities.Add(new City { Id = 1, Name = "Stockholm", Country = "SE" });
        db.SaveChanges();

        var mockHttp = new HttpClient(new MockHttpMessageHandler());
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "OpenWeatherMap:ApiKey", "dummy" }
            })
            .Build();

        var service = new WeatherService(mockHttp, db, config);

        await service.UpdateWeatherAsync();

        var saved = db.WeatherData.FirstOrDefault();
        Assert.NotNull(saved);
        Assert.Equal(12.3, saved.Temperature);
        Assert.Equal(1, saved.CityId);
    }
    [Fact]
    public async Task UpdateWeatherAsync_DoesNotSave_WhenApiReturnsEmptyMain()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var db = new AppDbContext(options);
        db.Cities.Add(new City { Id = 1, Name = "Stockholm", Country = "SE" });
        db.SaveChanges();

        var mockHttp = new HttpClient(new EmptyWeatherResponseHandler());
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
            { "OpenWeatherMap:ApiKey", "dummy" }
            })
            .Build();

        var service = new WeatherService(mockHttp, db, config);

        await service.UpdateWeatherAsync();

        Assert.Empty(db.WeatherData);
    }

}
