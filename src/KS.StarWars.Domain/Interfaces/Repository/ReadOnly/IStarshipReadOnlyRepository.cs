using KS.StarWars.Domain.Entities;
using System.Collections.Generic;

namespace KS.StarWars.Domain.Interfaces.Repository.ReadOnly
{
    /// <summary>
    /// Responsible for retrieving starships information.
    /// </summary>
    public interface IStarshipReadOnlyRepository
    {
        /// <summary>
        /// Get all starships.
        /// </summary>
        /// <returns>Collection of <see cref="Starship"/>.</returns>
        IEnumerable<Starship> GetAll();
    }
}
