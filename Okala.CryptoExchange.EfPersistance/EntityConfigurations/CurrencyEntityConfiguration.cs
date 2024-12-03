using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Okala.CryptoExchange.Domain;
using Okala.CryptoExchange.EfPersistance.Common;
namespace Okala.CryptoExchange.EfPersistance.EntityConfigurations
{
    public class CurrencyEntityConfiguration : EntityTypeConfiguration<Currency>
    {
        public CurrencyEntityConfiguration(string schema) : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Symbol).HasMaxLength(50);
            builder.Property(x => x.Code).HasMaxLength(50);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.HasMany(c => c.CryptoCurrencyQuotes) 
                   .WithOne(q => q.Currency)
                   .HasForeignKey(q => q.CurrencyId)
                   .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed  

            builder.ToTable(nameof(Currency), "dbo");
        }
    }
}
