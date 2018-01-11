using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KS.StarWars.Domain.Entities
{
    [DataContract(Name = nameof(StarLogPage))]
    public class StarLogPage
    {
        [DataMember(Name = "count")]
        public string Count { get; set; }

        [DataMember(Name = "next")]
        public string Next { get; set; }

        [DataMember(Name = "previous")]
        public string Previous { get; set; }

        [DataMember(Name = "results")]
        public IEnumerable<StarShip> Results { get; set; }
    }
}
