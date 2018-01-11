using KS.StarWars.Domain.Entities;
using System;

namespace KS.StarWars.Domain.Interfaces.Services
{
    public interface IStarlogService : IDisposable
    {
        StarLogPage GetStarshipsByPage(int? page = 1);
    }
}
