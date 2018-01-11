using System;
using System.Runtime.Serialization;

namespace KS.StarWars.Domain.Entities
{
    [DataContract(Name = "starship")]
    public class Starship
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "mglt")]
        public decimal MGLT { get; set; }
    }
}