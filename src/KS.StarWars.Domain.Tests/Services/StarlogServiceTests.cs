using AutoMoq;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using KS.StarWars.Domain.Services;
using Moq;
using Xunit;

namespace KS.StarWars.Domain.Tests.Services
{
    public class StarlogServiceTests
    {
        private readonly AutoMoqer mocker;

        public StarlogServiceTests()
        {
            mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "GetStarshipsByPage must execute repository method once")]
        [Trait("Starlog", "Domain Service")]
        public void Starlog_GetStarshipsByPage_MustExecuteRepositoryMethodOnce()
        {
            // Arrange
            mocker.Create<StarlogService>();

            var starlogService = mocker.Resolve<StarlogService>();
            var starlogPageReadOnlyRepository = mocker.GetMock<IStarlogPageReadOnlyRepository>();

            // Act
            starlogService.GetStarshipsByPage();

            // Assert
            starlogPageReadOnlyRepository.Verify(s => s.GetStarshipsByPage(It.IsAny<int>()), Times.Once());
        }
    }
}
