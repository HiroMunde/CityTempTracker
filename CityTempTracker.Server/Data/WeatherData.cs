namespace CityTempTracker.Server.Data
{
    public class WeatherData
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; } = null!;
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
