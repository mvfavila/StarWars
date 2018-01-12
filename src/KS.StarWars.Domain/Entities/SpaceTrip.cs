using KS.StarWars.Domain.Interfaces.Validation;
using KS.StarWars.Domain.Validation.SpaceTrip;
using KS.StarWars.Domain.ValueObjects;
using System.Collections.Generic;
using System;

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
        /// Get how many times a Star Ship will have to stop to resupply during a SpaceTrip.
        /// </summary>
        /// <param name="mglt">Maximum speed travelled by a Star Ships in Mega Lights in 1(one) hour.</param>
        /// <param name="consumables">Consumables used by a Star Ship per hour.</param>
        /// <returns>Resupply Stops quantity.</returns>
        public decimal GetResupplyStopsQuantity(decimal mglt, string consumables)
        {
            var consumablesInHours = ConvertoToHours(consumables);

            var dividend = GetDistance();
            var divisor = mglt * consumablesInHours;

            var quocient = dividend / divisor;

            quocient = Truncate(quocient);

            return decimal.Parse(quocient.ToString());
        }

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

        // Helper Methods

        private static decimal ConvertoToHours(string consumables)
        {
            const int ONE_HOUR = 1;
            const int HOURS_IN_ONE_DAY = 24;
            const int HOURS_IN_ONE_WEEK = 24 * 7;
            const int HOURS_IN_ONE_MONTH = 24 * 30;
            const int HOURS_IN_ONE_YEAR = 24 * 30 * 12;


            var data = consumables.Split(' ');
            var number = decimal.Parse(data[0]);
            var unit = data[1].ToLower();

            var amountOfHoursPerUnit = 1;

            switch (unit)
            {
                case "day":
                case "days":
                    amountOfHoursPerUnit = HOURS_IN_ONE_DAY;
                    break;
                case "week":
                case "weeks":
                    amountOfHoursPerUnit = HOURS_IN_ONE_WEEK;
                    break;
                case "month":
                case "months":
                    amountOfHoursPerUnit = HOURS_IN_ONE_MONTH;
                    break;
                case "year":
                case "years":
                    amountOfHoursPerUnit = HOURS_IN_ONE_YEAR;
                    break;
                default:
                    amountOfHoursPerUnit = ONE_HOUR;
                    break;
            }

            var result = number * amountOfHoursPerUnit;

            return result;
        }

        private static decimal Truncate(decimal quocient)
        {
            // Truncate is executed this way so Domain project does not have a referece to Math library.

            var split = quocient.ToString().Split('.');

            return decimal.Parse(split[0]);
        }
    }
}
