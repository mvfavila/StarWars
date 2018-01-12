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

        public Dictionary<string, string> GetAllResupplyStopsForSpaceTrip(SpaceTrip spaceTrip)
        {
            var starShips = GetAllStarships();

             ComputeSpacetripResupplyStops(spaceTrip, starShips);

            return spaceTrip.ResupplyStops;
        }

        // Helper methods

        private static void ComputeSpacetripResupplyStops(SpaceTrip spaceTrip, IEnumerable<StarShip> starShips)
        {
            foreach (var starShip in starShips)
            {
                if (!IsMgltNumeric(starShip, out decimal speed))
                {
                    AddUnknownResultMessage(spaceTrip, starShip);
                    continue;
                }

                var numberOfStops = ComputeStops(starShip, spaceTrip);

                spaceTrip.AddResupplyStop(starShip.Name, numberOfStops);
            }
        }

        private static void AddUnknownResultMessage(SpaceTrip spaceTrip, StarShip starShip)
        {
            spaceTrip.AddResupplyStop(starShip.Name, "Not available (MGLT = 'Unknown')");
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

            var result = spaceTrip.GetResupplyStopsQuantity(mglt, consumables);

            return FormatResult(result);
        }

        private static string FormatResult(decimal result)
        {
            var value = result.ToString();

            return value.Split('.')[0];
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
