using Bogus;
using KS.StarWars.Domain.Entities;
using System.Linq;
using Xunit;

namespace KS.StarWars.Domain.Tests.Validation
{
    public class SpaceTripIsVerifiedForRegistrationTests
    {
        /// <summary>
        /// Tests the SpaceShip validation (one hundred thousand instances)
        /// </summary>
        [Fact(DisplayName = "Valid instance")]
        [Trait(nameof(SpaceTrip), nameof(Validation))]
        public void SpaceTrip_Validation_MustBeValid()
        {
            // Arrange
            var spaceTrips = new Faker<SpaceTrip>()
                .CustomInstantiator(s => new SpaceTrip(
                    s.Random.Decimal(0.0M, decimal.MaxValue).ToString()
                    )).Generate(100000);

            // Act
            spaceTrips.ForEach(s => s.IsValid());

            // Assert
            Assert.Empty(spaceTrips.Where(s => !s.IsValid()));
        }

        /// <summary>
        /// Tests the SpaceShip validation with negative values (1 thousand instances)
        /// </summary>
        [Fact(DisplayName = "Distance can not be negative")]
        [Trait(nameof(SpaceTrip), nameof(Validation))]
        public void SpaceTrip_Validation_DistanceMustBeNonNegative()
        {
            // Arrange
            const int NUMBER_OF_INSTANCES = 1000;
            var spaceTrips = new Faker<SpaceTrip>()
                .CustomInstantiator(s => new SpaceTrip(
                    s.Random.Decimal(decimal.MinValue, -0.00000000001M).ToString()
                    )).Generate(NUMBER_OF_INSTANCES);

            // Act
            spaceTrips.ForEach(s => s.IsValid());

            // Assert
            Assert.Empty(spaceTrips.Where(s => s.IsValid()));
            Assert.Equal(NUMBER_OF_INSTANCES, spaceTrips.Count(
                s => s.ValidationResult.Errors.Select(
                    e => e.Message == "Distance can not be less than 0(zero)").Any()));
        }
    }
}
