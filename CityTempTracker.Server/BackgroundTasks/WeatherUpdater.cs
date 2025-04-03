using CityTempTracker.Server.Services;

namespace CityTempTracker.Server.BackgroundTasks
{
    public class WeatherUpdater : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public WeatherUpdater(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();
                    await weatherService.UpdateWeatherAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Weather update failed: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}