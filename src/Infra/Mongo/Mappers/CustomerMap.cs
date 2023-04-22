using Caesareum.ETournamentsApp.Core.Entities;
using Caesareum.ETournamentsApp.Infra.Mongo.Abstractions;
using Fwks.MongoDb.Mappers;

namespace Caesareum.ETournamentsApp.Infra.Mongo.Mappers;

public sealed class CustomerMap : EntityClassMap<CustomerDocument>, IEntityMap
{
}