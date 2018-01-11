using KS.StarWars.Data.HttpRest;
using System.Net;
using Xunit;

namespace KS.StarWars.Data.Tests.HttpRest
{
    public class HttpRestClientTests
    {
        [Fact(DisplayName = "Most be not null response")]
        [Trait(nameof(HttpRestClient), "Consume service")]
        public void HttpRestClient_Service_MustBeNotNullResponse()
        {
            // Arrange
            var httpClient = CreateHttpRestClient();

            // Act
            var response = httpClient.Get("starships/");

            // Assert
            Assert.NotNull(response);
        }

        [Fact(DisplayName = "Most return Not Found Exception")]
        [Trait(nameof(HttpRestClient), "Consume service")]
        public void HttpRestClient_Service_MustReturnNotFound()
        {
            // Arrange
            var httpClient = CreateHttpRestClient();

            // Act + Assert

            var ex = Assert.Throws<WebException>(() => httpClient.Get("albums/0"));

            Assert.Equal("The remote server returned an error: (404) Not Found.", ex.Message);
        }

        private static HttpRestClient CreateHttpRestClient()
        {
            const string remoteHost = "https://swapi.co/api/";
            var httpClient = new HttpRestClient(remoteHost);
            return httpClient;
        }
    }
}
