namespace CityTempTracker.Server.Data
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public ICollection<WeatherData> WeatherEntries { get; set; } = new List<WeatherData>();
    }
}
