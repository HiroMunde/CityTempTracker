using CityTempTracker.Server.Data;
using System.Text.Json;
using CityTempTracker.Server.Models;

namespace CityTempTracker.Server.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient httpClient, AppDbContext db, IConfiguration config)
        {
            _httpClient = httpClient;
            _db = db;
            _config = config;
        }

        public async Task UpdateWeatherAsync()
        {
            var apiKey = _config["OpenWeatherMap:ApiKey"];
            var cities = _db.Cities.ToList();

            foreach (var city in cities)
            {
                var response = await _httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={city.Name},{city.Country}&appid={apiKey}&units=metric");
                var data = JsonSerializer.Deserialize<WeatherApiResponse>(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (data?.Main == null)
                {
                    Console.WriteLine($"[WARNING] Empty weather response for {city.Name}, {city.Country}");
                    continue;
                }

                _db.WeatherData.Add(new WeatherData
                {
                    CityId = city.Id,
                    Temperature = data.Main.Temp,
                    Timestamp = DateTime.UtcNow
                });

                Console.WriteLine($"[INFO] Saved temperature for {city.Name}: {data.Main.Temp} °C");
            }
            await _db.SaveChangesAsync();
        }
    }
}