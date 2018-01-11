using KS.StarWars.Domain.Interfaces.Specification;
using System;

namespace KS.StarWars.Domain.Specification.SpaceTrip
{
    public class SpaceTripIsDistanceNonNegativeValue : ISpecification<Entities.SpaceTrip>
    {
        public bool IsSatisfiedBy(Entities.SpaceTrip spaceTrip)
        {
            return spaceTrip.Distance >= 0;
        }
    }
}
