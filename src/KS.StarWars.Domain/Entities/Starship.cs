using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KS.StarWars.Domain.Entities
{
    [DataContract(Name = "starship")]
    public class Starship
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "manufacturer")]
        public string Manufacturer { get; set; }

        [DataMember(Name = "cost_in_credits")]
        public string CostInCredits { get; set; }

        [DataMember(Name = "length")]
        public string Length { get; set; }

        [DataMember(Name = "max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }

        [DataMember(Name = "crew")]
        public string Crew { get; set; }

        [DataMember(Name = "passengers")]
        public string Passengers { get; set; }

        [DataMember(Name = "cargo_capacity")]
        public string CargoCapacity { get; set; }

        [DataMember(Name = "consumables")]
        public string Consumables { get; set; }

        [DataMember(Name = "hyperdrive_rating")]
        public string HyperdriveRating { get; set; }

        [DataMember(Name = "mglt")]
        public string Mglt { get; set; }

        [DataMember(Name = "starship_class")]
        public string StarshipClass { get; set; }

        [DataMember(Name = "pilots")]
        public IEnumerable<string> Pilots { get; set; }

        [DataMember(Name = "films")]
        public IEnumerable<string> Films { get; set; }

        [DataMember(Name = "created")]
        public string Created { get; set; }

        [DataMember(Name = "edited")]
        public string Edited { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}