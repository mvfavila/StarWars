using AutoMoq;
using Bogus;
using KS.StarWars.Application.AppService;
using KS.StarWars.Domain.Entities;
using KS.StarWars.Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace KS.StarWars.Application.Tests.AppService
{
    public class SpaceTripAppServiceTests
    {
        private readonly AutoMoqer mocker;

        public SpaceTripAppServiceTests()
        {
            mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "GetStarshipsByPage must return all Star Ships from page")]
        [Trait(nameof(SpaceTrip), "App Service")]
        public void SpaceTrip_GetStarshipsByPage_MustReturnAllStarShips()
        {
            // Arrange

            var starShips = new Faker<StarShip>()
                .CustomInstantiator(s => new StarShip
                {
                    Name = $"{s.Name.LastName()} {s.Random.Decimal(1, 100)}",
                    Model = s.Company.CompanyName(),
                    Manufacturer = s.Company.CompanyName(),
                    CostInCredits = s.Commerce.Price(1, 100000000),
                    Length = s.Random.Int(1, int.MaxValue).ToString(),
                    MaxAtmospheringSpeed = s.Random.Decimal(0.1M, 100000000M).ToString(),
                    Crew = s.Random.Int(1, int.MaxValue).ToString(),
                    Passengers = s.Random.Int(1, int.MaxValue).ToString(),
                    CargoCapacity = s.Random.Int(1, int.MaxValue).ToString(),
                    Consumables = $"{s.Random.Int(1, 100).ToString()} years",
                    HyperdriveRating = s.Random.Decimal(0.1M, 10.0M).ToString(),
                    Mglt = s.Random.Decimal(0.1M, 1000.0M).ToString(),
                    StarshipClass = s.Company.CompanyName(),
                    Pilots = new List<string>(),
                    Films = new List<string>(),
                    Created = DateTime.Now.ToString(),
                    Edited = DateTime.Now.ToString(),
                    Url = s.Internet.Url()
                }).Generate(1000);

            var starLogPage = new Faker<StarLogPage>()
                .CustomInstantiator(s => new StarLogPage
                    {
                        Count = starShips.Count.ToString(),
                        Next = null,
                        Previous = null,
                        Results = starShips
                }
                ).Generate();

            var spaceTrip = new Faker<SpaceTrip>()
                .CustomInstantiator(s => new SpaceTrip(
                    s.Random.Decimal(0, 99999999).ToString()
                    )).Generate();

            mocker.Create<SpaceTripAppService>();
            var spaceTripAppService = mocker.Resolve<SpaceTripAppService>();
            var starlogService = mocker.GetMock<IStarlogService>();
            starlogService
                .Setup(s => s.GetStarshipsByPage(It.IsAny<int>()))
                .Returns(starLogPage);

            // Act
            var resupplyStops = spaceTripAppService.GetAllResupplyStopsForSpaceTrip(spaceTrip);

            // Assert
            starlogService.Verify(s => s.GetStarshipsByPage(It.IsAny<int>()), Times.Once());
            Assert.Equal(int.Parse(starLogPage.Count), resupplyStops.Count);
        }
    }
}
