using System;
using Caesareum.ETournamentsApp.Core.Abstractions.Repositories;
using Caesareum.ETournamentsApp.Core.Entities;
using Fwks.MongoDb.Repositories;
using MongoDB.Driver;

namespace Caesareum.ETournamentsApp.Infra.Mongo.Repositories;

public sealed class CustomerRepository : BaseRepository<CustomerDocument>, ICustomerRepository<CustomerDocument, Guid>
{
    public CustomerRepository(
        IMongoDatabase database)
        : base(database, "customers")
    {
    }
}