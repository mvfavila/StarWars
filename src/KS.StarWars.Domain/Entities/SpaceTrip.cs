using System;
using KS.StarWars.Domain.Interfaces.Validation;
using KS.StarWars.Domain.ValueObjects;
using KS.StarWars.Domain.Validation.SpaceTrip;

namespace KS.StarWars.Domain.Entities
{
    /// <summary>
    /// Trip travelled by a StarWars spaceship.
    /// </summary>
    public class SpaceTrip : ISelfValidator
    {
        private SpaceTrip(decimal distance)
        {
            Distance = distance;
        }

        /// <summary>
        /// Distance of the space trip.
        /// </summary>
        public decimal Distance { get; private set; }

        /// <summary>
        /// Factory to add a new Space Trip to the travel log.
        /// </summary>
        /// <param name="distance">Distance of the space trip.</param>
        /// <returns>See <see cref="SpaceTrip"/>.</returns>
        public static SpaceTrip FactoryAdd(decimal distance)
        {
            return new SpaceTrip(distance);
        }

        /// <summary>
        /// Factory to test Space Trip instance.
        /// </summary>
        /// <param name="distance">Distance of the space trip.</param>
        /// <returns>See <see cref="SpaceTrip"/>.</returns>
        public static SpaceTrip FactoryTest(decimal distance)
        {
            return new SpaceTrip(distance);
        }

        public bool IsValid()
        {
            var validation = new SpaceTripIsVerifiedForRegistration();
            ValidationResult = validation.Validate(this);

            return ValidationResult.IsValid;
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}
