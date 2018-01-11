using System.Runtime.Serialization;

namespace KS.StarWars.Domain.Entities
{
    [DataContract(Name = "starship")]
    public class Starship
    {
        [DataMember(Name = "name")]
        public decimal Name { get; set; }

        [DataMember(Name = "mglt")]
        public decimal UserId { get; set; }
    }
}