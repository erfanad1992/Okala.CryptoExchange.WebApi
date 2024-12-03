using Okala.CryptoExchange.Domain.Common;

namespace Okala.CryptoExchange.Domain;

public class Currency : EntityBase<int>
{
    public Currency(
        int id,
        string code,
        string name,
        string symbol
        )
    {
        Id = id;
        Code = code;
        Name = name;
        Symbol = symbol;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Symbol { get; private set; }

    public virtual ICollection<CryptoCurrencyQuote> CryptoCurrencyQuotes { get; set; }

}
