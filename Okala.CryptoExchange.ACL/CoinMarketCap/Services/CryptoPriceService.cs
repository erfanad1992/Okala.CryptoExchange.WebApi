using Okala.CryptoExchange.Application.CoinMarketExchanges;

namespace Okala.CryptoExchange.ACL.CoinMarketCap.Services;

public class CryptoPriceService : ICryptoPriceService
{
    private readonly ICoinMarketCapExchangeFromApiService _exchangeFromApiService;

    public CryptoPriceService(ICoinMarketCapExchangeFromApiService exchangeFromApiService)
    {
        _exchangeFromApiService = exchangeFromApiService;
    }

    public async Task<CryptoPriceResponseDto> GetCryptoPriceQuote(string symbol,string convertSymbols)
    {
        try
        {
            var apiResult = await _exchangeFromApiService.GetCoinMarketCapData(symbol, convertSymbols);
            if (!apiResult.Data.ContainsKey(symbol))
            {
                throw new Exception("Cryptocurrency not found");
            }
            var crypto = apiResult.Data[symbol].FirstOrDefault();

            return new CryptoPriceResponseDto
            {
                CryptocurrencyName = crypto.Name,
                PriceInUSD = crypto.Quote.USD.Price,
                PriceInEUR = crypto.Quote.EUR.Price,
                PriceInBRL = crypto.Quote.BRL.Price,
                PriceInGBP = crypto.Quote.GBP.Price,
                PriceInAUD = crypto.Quote.AUD.Price
            };


        }
        catch (Exception)
        {

            throw;
        }
    }
}
