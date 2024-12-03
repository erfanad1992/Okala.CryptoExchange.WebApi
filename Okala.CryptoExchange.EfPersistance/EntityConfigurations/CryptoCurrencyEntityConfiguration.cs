using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Okala.CryptoExchange.Domain;
using Okala.CryptoExchange.EfPersistance.Common;

namespace Okala.CryptoExchange.EfPersistance.EntityConfigurations;

public class CryptoCurrencyEntityConfiguration : EntityTypeConfiguration<Cryptocurrency>
{
    public CryptoCurrencyEntityConfiguration(string schema) : base(schema)
    {
    }

    public override void Configure(EntityTypeBuilder<Cryptocurrency> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Symbol).HasMaxLength(50);
        builder.Property(x => x.Code).HasMaxLength(50);
        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasMany(c => c.CryptoCurrencyQuotes)
         .WithOne(q => q.Cryptocurrency)
         .HasForeignKey(q => q.CryptocurrencyId)
         .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(nameof(Cryptocurrency), "dbo");

    }
}
