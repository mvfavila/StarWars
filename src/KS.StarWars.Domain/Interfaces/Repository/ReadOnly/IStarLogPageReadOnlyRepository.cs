using KS.StarWars.Domain.Entities;
using System;

namespace KS.StarWars.Domain.Interfaces.Repository.ReadOnly
{
    /// <summary>
    /// Responsible for retrieving starlog pages information.
    /// </summary>
    public interface IStarlogPageReadOnlyRepository : IDisposable
    {
        /// <summary>
        /// Get all starships from a starlog page.
        /// </summary>
        /// <returns>See <see cref="StarLogPage"/>.</returns>
        StarLogPage GetStarshipsByPage(int? page = 1);
    }
}
