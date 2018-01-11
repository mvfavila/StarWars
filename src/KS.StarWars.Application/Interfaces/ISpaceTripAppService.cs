using KS.StarWars.Domain.Entities;
using System;
using System.Collections.Generic;

namespace KS.StarWars.Application.Interfaces
{
    public interface ISpaceTripAppService : IDisposable
    {
        IEnumerable<Tuple<string, int>> GetAllResuplyStopsForSpaceTrip(SpaceTrip spaceTrip);
    }
}
