﻿using KS.StarWars.Domain.Interfaces.Validation;
using KS.StarWars.Domain.ValueObjects;

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

        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public ValidationResult ValidationResult => throw new System.NotImplementedException();
    }
}
