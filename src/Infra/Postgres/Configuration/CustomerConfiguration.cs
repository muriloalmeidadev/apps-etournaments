using Caesareum.ETournamentsApp.Core.Entities;
using Fwks.Postgres.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caesareum.ETournamentsApp.Infra.Postgres.Configuration;

public sealed class CustomerConfiguration : EntityTypeConfiguration<CustomerEntity, int>
{
    public override void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        base.Configure(builder);

        builder
            .ToTable("Customers", "App");
    }
}