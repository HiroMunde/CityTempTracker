using CityTempTracker.Server.BackgroundTasks;
using CityTempTracker.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CityTempTracker.Tests
{
    public class WeatherUpdaterTests
    {
        [Fact]
        public async Task ExecuteAsync_CallsUpdateWeatherAsync()
        {
            var weatherServiceMock = new Mock<IWeatherService>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IWeatherService)))
                .Returns(weatherServiceMock.Object);

            var scopeMock = new Mock<IServiceScope>();
            scopeMock.Setup(s => s.ServiceProvider).Returns(serviceProviderMock.Object);

            var scopeFactoryMock = new Mock<IServiceScopeFactory>();
            scopeFactoryMock.Setup(f => f.CreateScope()).Returns(scopeMock.Object);

            var updater = new WeatherUpdater(scopeFactoryMock.Object);

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            
            await updater.StartAsync(cts.Token);
            
            weatherServiceMock.Verify(ws => ws.UpdateWeatherAsync(), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ExecuteAsync_HandlesException_Gracefully()
        {
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock
                .Setup(ws => ws.UpdateWeatherAsync())
                .ThrowsAsync(new Exception("Simulated error"));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IWeatherService)))
                .Returns(weatherServiceMock.Object);

            var scopeMock = new Mock<IServiceScope>();
            scopeMock.Setup(s => s.ServiceProvider).Returns(serviceProviderMock.Object);

            var scopeFactoryMock = new Mock<IServiceScopeFactory>();
            scopeFactoryMock.Setup(f => f.CreateScope()).Returns(scopeMock.Object);

            var updater = new WeatherUpdater(scopeFactoryMock.Object);

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            
            var ex = await Record.ExceptionAsync(() => updater.StartAsync(cts.Token));
            
            Assert.Null(ex);
        }
    }
}
