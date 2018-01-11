using KS.StarWars.Application.AppService;
using KS.StarWars.Application.Interfaces;
using KS.StarWars.Data.HttpRest;
using KS.StarWars.Data.Interfaces;
using KS.StarWars.Data.Repositories.StarLogPage;
using KS.StarWars.Domain.Interfaces.Repository.ReadOnly;
using KS.StarWars.Domain.Interfaces.Services;
using KS.StarWars.Domain.Services;
using SimpleInjector;

namespace KS.StarWars.CrossCutting.IoC
{
    public static class StarWarsDependencyInjenction
    {
        public static Container container;

        public static void RegisterServices()
        {
            container = new Container();

            // Infra - Data

            container.Register<IStarlogPageReadOnlyRepository>(
                () => new StarlogPageReadOnlyRepository("starships/", new HttpRestClient("https://swapi.co/api/")),
                Lifestyle.Singleton);

            // Domain

            container.Register<IStarlogService, StarlogService>(Lifestyle.Singleton);

            // Application

            container.Register<ISpaceTripAppService, SpaceTripAppService>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
