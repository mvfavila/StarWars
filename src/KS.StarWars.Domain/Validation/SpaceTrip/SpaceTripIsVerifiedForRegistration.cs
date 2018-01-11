using KS.StarWars.Domain.Specification.SpaceTrip;
using KS.StarWars.Domain.Validation.Base;

namespace KS.StarWars.Domain.Validation.SpaceTrip
{
    public class SpaceTripIsVerifiedForRegistration : BaseSupervisor<Entities.SpaceTrip>
    {
        public SpaceTripIsVerifiedForRegistration()
        {
            var isDistanceNonNegativeValue = new SpaceTripIsDistanceNonNegativeValue();

            base.AddRule("IsDistanceNonNegativeValue", new Rule<Entities.SpaceTrip>(isDistanceNonNegativeValue,
               $"{nameof(Entities.SpaceTrip.Distance)} can not be less than 0 (zero)"));
        }
    }
}
