using Okala.CryptoExchange.Domain.Common;

namespace Okala.CryptoExchange.Domain;

public class CryptoCurrencyQuote : EntityBase<int>
{
    public CryptoCurrencyQuote(
        int cryptocurrencyId,
        int currencyId,
        decimal price,
        DateTime timestamp,
        int id
        )
    {
        CryptocurrencyId = cryptocurrencyId;
        CurrencyId = currencyId;
        Price = price;
        Timestamp = timestamp;
        Id = id;
    }
    public decimal Price { get; private set; }
    public DateTime Timestamp { get; private set; }

    public int CryptocurrencyId { get; private set; }
    public virtual Cryptocurrency Cryptocurrency { get; private set; }

    public int CurrencyId { get; private set; }
    public virtual Currency Currency { get; private set; }


}
