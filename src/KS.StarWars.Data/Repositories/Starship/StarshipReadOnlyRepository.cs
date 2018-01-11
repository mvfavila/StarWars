using KS.StarWars.Data.HttpRest;
using KS.StarWars.Data.Interfaces;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace KS.StarWars.Data.Repositories.Starship
{
    public class StarshipReadOnlyRepository : IStarshipReadOnlyRepository
    {
        private readonly IHttpRestClient httpRestClient;
        private readonly string resource;
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        public StarshipReadOnlyRepository(string resource, IHttpRestClient httpRestClient)
        {
            if (string.IsNullOrWhiteSpace(resource))
                throw new ArgumentNullException(nameof(resource));
            if (httpRestClient == null)
                throw new ArgumentNullException(nameof(httpRestClient));

            this.resource = resource;
            this.httpRestClient = httpRestClient;
        }

        public IEnumerable<Domain.Entities.Starship> GetAll()
        {
            var response = httpRestClient.Get(resource);

            var responseContent = HttpRestClientHelper.GetResultStreamAsString(response);

            var starships = jsonSerializer.Deserialize<IEnumerable<Domain.Entities.Starship>>(responseContent);

            return starships;
        }
    }
}
