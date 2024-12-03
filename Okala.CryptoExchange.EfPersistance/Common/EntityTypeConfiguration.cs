using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Okala.CryptoExchange.Domain.Common;

namespace Okala.CryptoExchange.EfPersistance.Common;
public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase<int>
{

    internal string Schema { get; }

    public EntityTypeConfiguration(string schema)
    {
        Schema = schema;
    }

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}



