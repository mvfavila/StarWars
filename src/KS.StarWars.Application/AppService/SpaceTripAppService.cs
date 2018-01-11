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
            this.starlogService = starlogService;
        }

        public Dictionary<string, int> GetAllResuplyStopsForSpaceTrip(SpaceTrip spaceTrip)
        {
            var starships = GetAllStarships();

             ComputeSpacetripResuplyStops(spaceTrip, starships);

            return spaceTrip.ResuplyStops;
        }

        private static void ComputeSpacetripResuplyStops(SpaceTrip spaceTrip, IEnumerable<Starship> starships)
        {
            var existingLog = new Dictionary<decimal, int>();

            foreach (var starship in starships)
            {
                var speed = decimal.Parse(starship.Mglt);

                int numberOfStops;
                
                if (IsNumberOfStopsAlreadyComputed(existingLog, speed))
                {
                    numberOfStops = existingLog[speed];
                }
                else
                {
                    numberOfStops = ComputeStops(speed, spaceTrip);
                    existingLog.Add(speed, numberOfStops);
                }

                spaceTrip.AddResuplyStop(starship.Name, numberOfStops);
            }
        }

        private static int ComputeStops(decimal speed, SpaceTrip spaceTrip)
        {
            return int.Parse(Math.Ceiling(spaceTrip.GetDistance() / speed).ToString());
        }

        private static bool IsNumberOfStopsAlreadyComputed(Dictionary<decimal, int> existingLog, decimal speed)
        {
            return existingLog.ContainsKey(speed);
        }

        private IEnumerable<Starship> GetAllStarships()
        {
            var page = 1;
            var starships = new List<Starship>();
            var next = string.Empty;
            while(next != null)
            {
                var starlogPage = starlogService.GetStarshipsByPage(page++);
                next = starlogPage.Next;
                starships.AddRange(starlogPage.Starships);
            }

            return starships;
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
