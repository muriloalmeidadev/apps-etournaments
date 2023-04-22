using System;
using Caesareum.ETournamentsApp.Core.Abstractions.Repositories;
using Caesareum.ETournamentsApp.Core.Entities;
using Caesareum.ETournamentsApp.Core.Settings;
using Caesareum.ETournamentsApp.Infra.Mongo.Abstractions;
using Caesareum.ETournamentsApp.Infra.Mongo.Repositories;
using Fwks.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Caesareum.ETournamentsApp.Infra.Mongo;

public static class ConfigureServices
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, AppSettings appSettings)
    {
        return services
             .AddMongoDb<IEntityMap>(appSettings.Storage.MongoDb, appSettings.Storage.MongoDb.Database)
             .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerRepository<CustomerDocument, Guid>, CustomerRepository>();
    }
}