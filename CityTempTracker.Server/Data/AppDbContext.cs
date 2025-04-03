using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CityTempTracker.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<WeatherData> WeatherData { get; set; } = null!;
    }
}
