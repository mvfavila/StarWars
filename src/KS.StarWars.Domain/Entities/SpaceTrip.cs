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
            ResuplyStops = new Dictionary<string, int>();
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
        /// Collection of quantity of resuply stops planned for each starship.
        /// </summary>
        public Dictionary<string, int> ResuplyStops { get; private set; }

        /// <summary>
        /// Add a Resuply Stops Entry for a Starship.
        /// </summary>
        /// <param name="name">Name of the Starship.</param>
        /// <param name="stops">Number of resuply stops planned.</param>
        public void AddResuplyStop(string name, int stops)
        {
            ResuplyStops.Add(name, stops);
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
