using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Okala.CryptoExchange.Domain;
using Okala.CryptoExchange.EfPersistance.Common;

namespace Okala.CryptoExchange.EfPersistance.EntityConfigurations;

public class CryptoCurrencyQuoteEntityConfiguration : EntityTypeConfiguration<CryptoCurrencyQuote>
{
    public CryptoCurrencyQuoteEntityConfiguration(string schema) : base(schema)
    {
    }

    public override void Configure(EntityTypeBuilder<CryptoCurrencyQuote> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
               .HasPrecision(18, 8)
               .IsRequired();

        builder.HasOne(q => q.Cryptocurrency)
             .WithMany(c => c.CryptoCurrencyQuotes)
             .HasForeignKey(q => q.CryptocurrencyId)
             .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(q => q.Currency)
               .WithMany(c => c.CryptoCurrencyQuotes)
               .HasForeignKey(q => q.CurrencyId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(nameof(CryptoCurrencyQuote), "dbo");
    }
}
