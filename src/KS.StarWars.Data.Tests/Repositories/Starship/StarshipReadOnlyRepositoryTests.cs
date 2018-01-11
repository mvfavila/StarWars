using AutoMoq;
using Bogus;
using KS.StarWars.Data.HttpRest;
using KS.StarWars.Data.Interfaces;
using KS.StarWars.Data.Repositories.Starship;
using KS.StarWars.Domain.Entities;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using Moq;
using System.Linq;
using System.Web.Script.Serialization;
using Xunit;

namespace KS.StarWars.Data.Tests.Repositories.Starship
{
    public class StarshipReadOnlyRepositoryTests
    {
        private const string RESOURCE = "dummyResource";
        private readonly AutoMoqer mocker;

        public StarshipReadOnlyRepositoryTests()
        {
            mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "Most be not null response")]
        [Trait(nameof(StarshipReadOnlyRepository), "Consume service")]
        public void StarshipReadOnlyRepository_GetAll_MustReturn2Starships()
        {
            // Arrange
            var starships = new Faker<Domain.Entities.Starship>()
                .CustomInstantiator(s => new Domain.Entities.Starship
                {
                    Name = s.Name.LastName(),
                    MGLT = s.Random.Decimal(0.000001M, 1000)
                }
                ).Generate(2);
            mocker.Create<IHttpRestClient>();
            var httpRestClientMock = mocker.GetMock<IHttpRestClient>();
            var mockResponse = TestHelper.MockWebResponse(
                new JavaScriptSerializer().Serialize(starships));
            httpRestClientMock.Setup(h => h.Get(RESOURCE)).Returns(mockResponse);

            var httpAlbumFinder = new StarshipReadOnlyRepository(RESOURCE, httpRestClientMock.Object);

            // Act
            var result = httpAlbumFinder.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
