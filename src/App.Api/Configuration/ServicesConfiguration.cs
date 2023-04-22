using Caesareum.ETournamentsApp.Application;
using Caesareum.ETournamentsApp.Core.Settings;
using Caesareum.ETournamentsApp.Infra.Mongo;
using Caesareum.ETournamentsApp.Infra.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Caesareum.ETournamentsApp.App.Api.Configuration;

internal static class DependenciesConfiguration
{
    internal static IServiceCollection AddDependencies(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddApplicationServices()
            .AddMongoDb(appSettings)
            .AddPostgres(appSettings);
    }
}