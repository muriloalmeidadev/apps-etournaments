using Caesareum.ETournamentsApp.Core.Abstractions.Repositories;
using Caesareum.ETournamentsApp.Core.Entities;
using Caesareum.ETournamentsApp.Core.Settings;
using Caesareum.ETournamentsApp.Infra.Postgres.Contexts;
using Caesareum.ETournamentsApp.Infra.Postgres.Repositories;
using Fwks.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Caesareum.ETournamentsApp.Infra.Postgres;

public static class ConfigureServices
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddPostgres<AppServiceContext>(appSettings.Storage.Postgres)
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerRepository<CustomerEntity, int>, CustomerRepository>();
    }
}