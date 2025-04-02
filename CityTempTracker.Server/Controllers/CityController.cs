namespace CityTempTracker.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using global::CityTempTracker.Server.Data;

    namespace CityTempTracker.Server.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CityController : ControllerBase
        {
            private readonly AppDbContext _db;

            public CityController(AppDbContext db)
            {
                _db = db;
            }

            [HttpGet]
            public IActionResult GetCities()
            {
                var cities = _db.Cities
                    .Select(c => new { c.Id, c.Name, c.Country })
                    .ToList();

                return Ok(cities);
            }
        }
    }

}
