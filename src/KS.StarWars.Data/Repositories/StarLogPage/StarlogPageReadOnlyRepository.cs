using KS.StarWars.Data.HttpRest;
using KS.StarWars.Data.Interfaces;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using System;
using System.Web.Script.Serialization;

namespace KS.StarWars.Data.Repositories.StarLogPage
{
    public class StarlogPageReadOnlyRepository : IStarlogPageReadOnlyRepository
    {
        private readonly IHttpRestClient httpRestClient;
        private readonly string resource;
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        public StarlogPageReadOnlyRepository(string resource, IHttpRestClient httpRestClient)
        {
            if (string.IsNullOrWhiteSpace(resource))
                throw new ArgumentNullException(nameof(resource));
            if (httpRestClient == null)
                throw new ArgumentNullException(nameof(httpRestClient));

            this.resource = resource;
            this.httpRestClient = httpRestClient;
        }

        public Domain.Entities.StarLogPage GetStarshipsByPage(int? page = 1)
        {
            var param = $"?page={page}";

            var response = httpRestClient.Get(resource, param);

            var responseContent = HttpRestClientHelper.GetResultStreamAsString(response);

            var starlogPage = jsonSerializer.Deserialize<Domain.Entities.StarLogPage>(responseContent);

            return starlogPage;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
