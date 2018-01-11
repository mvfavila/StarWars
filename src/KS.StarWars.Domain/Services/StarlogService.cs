using KS.StarWars.Domain.Entities;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using KS.StarWars.Domain.Interfaces.Services;
using System;

namespace KS.StarWars.Domain.Services
{
    public class StarlogService : IStarlogService
    {
        private readonly IStarlogPageReadOnlyRepository starlogPageReadOnlyRepository;

        public StarlogService(IStarlogPageReadOnlyRepository starlogPageReadOnlyRepository)
        {
            this.starlogPageReadOnlyRepository = starlogPageReadOnlyRepository;
        }

        public StarLogPage GetStarshipsByPage(int? page = 1)
        {
            return starlogPageReadOnlyRepository.GetStarshipsByPage(page);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            starlogPageReadOnlyRepository.Dispose();
        }

        ~StarlogService()
        {
            Dispose(false);
        }
    }
}
