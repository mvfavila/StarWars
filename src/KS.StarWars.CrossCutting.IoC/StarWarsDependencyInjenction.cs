using KS.StarWars.Application.AppService;
using KS.StarWars.Application.Interfaces;
using KS.StarWars.Data.HttpRest;
using KS.StarWars.Data.Repositories.StarLogPage;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using KS.StarWars.Domain.Interfaces.Services;
using KS.StarWars.Domain.Services;
using SimpleInjector;
using System.Configuration;

namespace KS.StarWars.CrossCutting.IoC
{
    public static class StarWarsDependencyInjenction
    {
        public static Container container;
        private static string UrlRemote = ConfigurationManager.AppSettings["UrlRemote"];
        private static string ResourceStarShips = ConfigurationManager.AppSettings["ResourceStarShips"];

        public static void RegisterServices()
        {
            container = new Container();

            // Infra - Data

            container.Register<IStarlogPageReadOnlyRepository>(
                () => new StarlogPageReadOnlyRepository(ResourceStarShips, new HttpRestClient(UrlRemote)),
                Lifestyle.Singleton);

            // Domain

            container.Register<IStarlogService, StarlogService>(Lifestyle.Singleton);

            // Application

            container.Register<ISpaceTripAppService, SpaceTripAppService>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
