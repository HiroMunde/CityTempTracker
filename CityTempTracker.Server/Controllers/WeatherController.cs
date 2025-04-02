using CityTempTracker.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CityTempTracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly AppDbContext _db;

        public WeatherController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("city/{cityId}")]
        public IActionResult GetWeatherHistory(int cityId)
        {
            var weatherData = _db.WeatherData
                .Where(w => w.CityId == cityId)
                .OrderByDescending(w => w.Timestamp)
                .Take(100)
                .Select(w => new
                {
                    w.Timestamp,
                    w.Temperature,
                    w.City.Name,
                    w.City.Country
                })
                .ToList();

            return Ok(weatherData);
        }
    }
}