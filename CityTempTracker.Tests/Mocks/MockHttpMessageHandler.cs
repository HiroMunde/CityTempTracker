using System.Net;
using System.Text;

namespace CityTempTracker.Tests.Mocks
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var json = @"{
            ""main"": { ""temp"": 12.3 },
            ""name"": ""Stockholm"",
            ""sys"": { ""country"": ""SE"" }
        }";

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        }
    }
}
