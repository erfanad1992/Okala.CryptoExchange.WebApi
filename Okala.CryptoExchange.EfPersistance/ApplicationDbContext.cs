using Microsoft.EntityFrameworkCore;
using Okala.CryptoExchange.Domain;
using Okala.CryptoExchange.EfPersistance.EntityConfigurations;

namespace Okala.CryptoExchange.EfPersistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<Cryptocurrency> CryptoCurrencies { get; set; }
    public virtual DbSet<CryptoCurrencyQuote> CryptoCurrencyQuotes { get; set; }
    public virtual DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CryptoCurrencyEntityConfiguration("dbo"));
        modelBuilder.ApplyConfiguration(new CurrencyEntityConfiguration("dbo"));

        base.OnModelCreating(modelBuilder);
    }
}
