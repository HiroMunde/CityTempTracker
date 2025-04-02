namespace CityTempTracker.Server.Models
{
    public class WeatherApiResponse
    {
        public MainData Main { get; set; } = null!;
        public string Name { get; set; } = null!;
        public SysData Sys { get; set; } = null!;
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
    }

    public class SysData
    {
        public string Country { get; set; } = null!;
    }
}
