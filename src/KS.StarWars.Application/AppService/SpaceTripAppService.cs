using KS.StarWars.Application.Interfaces;
using KS.StarWars.Domain.Entities;
using System;
using System.Collections.Generic;

namespace KS.StarWars.Application.AppService
{
    public class SpaceTripAppService : ISpaceTripAppService
    {
        public IEnumerable<Tuple<string, int>> GetAllResuplyStopsForSpaceTrip(SpaceTrip spaceTrip)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }

        ~SpaceTripAppService()
        {
            Dispose(false);
        }
    }
}
