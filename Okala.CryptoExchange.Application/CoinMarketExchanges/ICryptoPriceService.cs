namespace Okala.CryptoExchange.Application.CoinMarketExchanges;

public interface ICryptoPriceService
{
    Task<CryptoPriceResponseDto> GetCryptoPriceQuote(string symbol, string convertSymbols);
}
