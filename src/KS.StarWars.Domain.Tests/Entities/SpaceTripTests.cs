using KS.StarWars.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KS.StarWars.Domain.Tests.Entities
{
    public class SpaceTripTests
    {
        [Fact(DisplayName = "GetResupplyStopsQuantity must return 74")]
        [Trait(nameof(SpaceTrip), "Domain Service")]
        public void SpaceTrip_GetResupplyStopsQuantity_MustReturn74()
        {
            // Arrange
            const string DISTANCE = "1000000";
            const decimal MGLT = 80;
            const string CONSUMABLES = "1 week";
            const int EXPECTED_RESULT = 74;
            var spaceTrip = new SpaceTrip(DISTANCE);

            // Act
            var resupplyStops = spaceTrip.GetResupplyStopsQuantity(MGLT, CONSUMABLES);

            // Assert
            Assert.Equal(EXPECTED_RESULT, resupplyStops);
        }

        [Fact(DisplayName = "GetResupplyStopsQuantity must return 9")]
        [Trait(nameof(SpaceTrip), "Domain Service")]
        public void SpaceTrip_GetResupplyStopsQuantity_MustReturn9()
        {
            // Arrange
            const string DISTANCE = "1000000";
            const decimal MGLT = 75;
            const string CONSUMABLES = "2 months";
            const int EXPECTED_RESULT = 9;
            var spaceTrip = new SpaceTrip(DISTANCE);

            // Act
            var resupplyStops = spaceTrip.GetResupplyStopsQuantity(MGLT, CONSUMABLES);

            // Assert
            Assert.Equal(EXPECTED_RESULT, resupplyStops);
        }

        [Fact(DisplayName = "GetResupplyStopsQuantity must return 11")]
        [Trait(nameof(SpaceTrip), "Domain Service")]
        public void SpaceTrip_GetResupplyStopsQuantity_MustReturn11()
        {
            // Arrange
            const string DISTANCE = "1000000";
            const decimal MGLT = 20;
            const string CONSUMABLES = "6 months";
            const int EXPECTED_RESULT = 11;
            var spaceTrip = new SpaceTrip(DISTANCE);

            // Act
            var resupplyStops = spaceTrip.GetResupplyStopsQuantity(MGLT, CONSUMABLES);

            // Assert
            Assert.Equal(EXPECTED_RESULT, resupplyStops);
        }

        [Fact(DisplayName = "GetResupplyStopsQuantity must return 1")]
        [Trait(nameof(SpaceTrip), "Domain Service")]
        public void SpaceTrip_GetResupplyStopsQuantity_MustReturn1()
        {
            // Arrange
            const string DISTANCE = "100";
            const decimal MGLT = 10;
            const string CONSUMABLES = "10 hours";
            const int EXPECTED_RESULT = 1;
            var spaceTrip = new SpaceTrip(DISTANCE);

            // Act
            var resupplyStops = spaceTrip.GetResupplyStopsQuantity(MGLT, CONSUMABLES);

            // Assert
            Assert.Equal(EXPECTED_RESULT, resupplyStops);
        }

        [Fact(DisplayName = "GetResupplyStopsQuantity must return 0")]
        [Trait(nameof(SpaceTrip), "Domain Service")]
        public void SpaceTrip_GetResupplyStopsQuantity_MustReturn0()
        {
            // Arrange
            const string DISTANCE = "100";
            const decimal MGLT = 10.00000000001M;
            const string CONSUMABLES = "10 hours";
            const int EXPECTED_RESULT = 0;
            var spaceTrip = new SpaceTrip(DISTANCE);

            // Act
            var resupplyStops = spaceTrip.GetResupplyStopsQuantity(MGLT, CONSUMABLES);

            // Assert
            Assert.Equal(EXPECTED_RESULT, resupplyStops);
        }

        [Fact(DisplayName = "Distance = decimal.MaxValue must return value")]
        [Trait(nameof(SpaceTrip), "Domain Service")]
        public void SpaceTrip_GetResupplyStopsQuantityMaxDecimalDistance_Success()
        {
            // Arrange
            var distance = decimal.MaxValue.ToString();
            const decimal MGLT = 10.00000000001M;
            const string CONSUMABLES = "10 years";
            var spaceTrip = new SpaceTrip(distance);

            // Act
            var resupplyStops = spaceTrip.GetResupplyStopsQuantity(MGLT, CONSUMABLES);

            // Assert
            Assert.True(resupplyStops > 0);
        }
    }
}
