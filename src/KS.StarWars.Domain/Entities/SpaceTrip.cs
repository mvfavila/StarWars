using KS.StarWars.Domain.Interfaces.Validation;
using KS.StarWars.Domain.Validation.SpaceTrip;
using KS.StarWars.Domain.ValueObjects;
using System.Collections.Generic;

namespace KS.StarWars.Domain.Entities
{
    /// <summary>
    /// Trip travelled by a StarWars spaceship.
    /// </summary>
    public class SpaceTrip : ISelfValidator
    {
        public SpaceTrip(string distance)
        {
            Distance = distance;
            ResupplyStops = new Dictionary<string, string>();
        }

        /// <summary>
        /// Distance of the space trip.
        /// </summary>
        internal string Distance { get; private set; }

        /// <summary>
        /// Get the decimal value of the SpaceTrip Distance.
        /// </summary>
        /// <returns>Distance.</returns>
        public decimal GetDistance()
        {
            return decimal.Parse(Distance);
        }

        /// <summary>
        /// Collection of quantity of resupply stops planned for each starship.
        /// </summary>
        public Dictionary<string, string> ResupplyStops { get; private set; }

        /// <summary>
        /// Add a Resupply Stops Entry for a Starship.
        /// </summary>
        /// <param name="name">Name of the Starship.</param>
        /// <param name="stops">Number of resupply stops planned.</param>
        public void AddResupplyStop(string name, string stops)
        {
            ResupplyStops.Add(name, stops);
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
