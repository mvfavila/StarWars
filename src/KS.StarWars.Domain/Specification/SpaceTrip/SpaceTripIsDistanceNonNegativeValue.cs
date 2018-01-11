using KS.StarWars.Domain.Interfaces.Specification;

namespace KS.StarWars.Domain.Specification.SpaceTrip
{
    public class SpaceTripIsDistanceNonNegativeValue : ISpecification<Entities.SpaceTrip>
    {
        public bool IsSatisfiedBy(Entities.SpaceTrip spaceTrip)
        {
            return decimal.Parse(spaceTrip.Distance) >= 0;
        }
    }
}
