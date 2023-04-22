using Caesareum.ETournamentsApp.Core.Abstractions.Repositories;
using Caesareum.ETournamentsApp.Core.Entities;
using Caesareum.ETournamentsApp.Infra.Postgres.Contexts;
using Fwks.Postgres.Repositories;

namespace Caesareum.ETournamentsApp.Infra.Postgres.Repositories;

public sealed class CustomerRepository : BaseRepository<CustomerEntity, int>, ICustomerRepository<CustomerEntity, int>
{
    public CustomerRepository(
        AppServiceContext dbContext) : base(dbContext)
    {
    }
}