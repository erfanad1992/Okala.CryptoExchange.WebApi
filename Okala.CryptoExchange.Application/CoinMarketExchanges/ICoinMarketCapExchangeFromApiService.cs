using Okala.CryptoExchange.Application.CoinMarketExchanges.ApiResponseDtos;

namespace Okala.CryptoExchange.Application.CoinMarketExchanges;

public interface ICoinMarketCapExchangeFromApiService
{
    Task<ApiResponseModel> GetCoinMarketCapData(string symbol, string convertSymbols);
}
