using System.Net;

namespace CityTempTracker.Tests
{
    public class ErrorResponseHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Internal Server Error (simulated)")
            };

            return Task.FromResult(response);
        }
    }
}