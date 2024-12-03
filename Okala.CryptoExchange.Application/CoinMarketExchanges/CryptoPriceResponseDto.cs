namespace Okala.CryptoExchange.Application.CoinMarketExchanges;

public class CryptoPriceResponseDto
{
    public string CryptocurrencyName { get; set; }
    public decimal PriceInUSD { get; set; }
    public decimal PriceInEUR { get; set; }
    public decimal PriceInBRL { get; set; }
    public decimal PriceInGBP { get; set; }
    public decimal PriceInAUD { get; set; }
}
