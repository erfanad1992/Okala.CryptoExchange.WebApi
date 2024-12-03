using Okala.CryptoExchange.Domain.Common;

namespace Okala.CryptoExchange.Domain;

public class Cryptocurrency :EntityBase<int>
{
    public Cryptocurrency(
        int id,
        string code,
        string name,
        string symbol,
        DateTime lastUpdated
        )
    {
        Id = id;
        Code = code;
        Name = name;
        Symbol = symbol;
        LastUpdated = lastUpdated;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public DateTime LastUpdated { get; private set; }

    public virtual ICollection<CryptoCurrencyQuote> CryptoCurrencyQuotes { get; set; }
    public void Update()
    {

    }

}