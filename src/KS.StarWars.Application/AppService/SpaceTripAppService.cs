using KS.StarWars.Application.Interfaces;
using KS.StarWars.Domain.Entities;
using KS.StarWars.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace KS.StarWars.Application.AppService
{
    public class SpaceTripAppService : ISpaceTripAppService
    {
        private readonly IStarlogService starlogService;

        public SpaceTripAppService(IStarlogService starlogService)
        {
            if (starlogService == null)
                throw new ArgumentNullException(nameof(starlogService));

            this.starlogService = starlogService;
        }

        public Dictionary<string, string> GetAllResuplyStopsForSpaceTrip(SpaceTrip spaceTrip)
        {
            var starShips = GetAllStarships();

             ComputeSpacetripResuplyStops(spaceTrip, starShips);

            return spaceTrip.ResuplyStops;
        }

        private static void ComputeSpacetripResuplyStops(SpaceTrip spaceTrip, IEnumerable<StarShip> starShips)
        {
            var existingLog = new Dictionary<decimal, string>();

            foreach (var starShip in starShips)
            {
                if (!IsMgltNumeric(starShip, out decimal speed))
                {
                    AddUnknownResultMessage(spaceTrip, starShip);
                    continue;
                }

                string numberOfStops;

                if (IsNumberOfStopsAlreadyComputed(existingLog, speed))
                {
                    numberOfStops = existingLog[speed];
                }
                else
                {
                    numberOfStops = ComputeStops(starShip, spaceTrip);
                    existingLog.Add(speed, numberOfStops);
                }

                spaceTrip.AddResuplyStop(starShip.Name, numberOfStops);
            }
        }

        private static void AddUnknownResultMessage(SpaceTrip spaceTrip, StarShip starShip)
        {
            spaceTrip.AddResuplyStop(starShip.Name, "Not available (MGLT = 'Unknown')");
        }

        private static bool IsMgltNumeric(StarShip starShip, out decimal speed)
        {
            return decimal.TryParse(starShip.Mglt, out speed);
        }

        private static string ComputeStops(StarShip starShip, SpaceTrip spaceTrip)
        {
            if (!decimal.TryParse(starShip.Mglt, out decimal mglt))
            {
                return "Not available (MGLT = 'unknown')";
            }
            var consumables = starShip.Consumables.ToLower();
            if(consumables == "unknown")
            {
                return "Not available (consumables = 'unknown')";
            }
            var consumablesInHours = ConvertoToHours(consumables);

            var dividend = spaceTrip.GetDistance();
            var divisor = mglt * consumablesInHours;

            return int.Parse(Math.Truncate(dividend / divisor).ToString()).ToString();
        }

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

        private static bool IsNumberOfStopsAlreadyComputed(Dictionary<decimal, string> existingLog, decimal speed)
        {
            return existingLog.ContainsKey(speed);
        }

        private IEnumerable<StarShip> GetAllStarships()
        {
            var page = 1;
            var starShips = new List<StarShip>();
            var next = string.Empty;
            while(next != null)
            {
                var starlogPage = starlogService.GetStarshipsByPage(page++);
                next = starlogPage.Next;
                if(starlogPage.Results != null)
                    starShips.AddRange(starlogPage.Results);
            }

            return starShips;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            starlogService.Dispose();
        }

        ~SpaceTripAppService()
        {
            Dispose(false);
        }
    }
}
