using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Okala.CryptoExchange.Application.CoinMarketExchanges;
using Okala.CryptoExchange.Application.CoinMarketExchanges.ApiResponseDtos;
using System.Text;
using System.Text.Json;

namespace Okala.CryptoExchange.ACL.CoinMarketCap.Services;

public class CoinMarketCapExchangeFromApiService : ICoinMarketCapExchangeFromApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly CoinMarketCapApiOptions _coinMarketCapApiOptions;


    public CoinMarketCapExchangeFromApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IOptions<CoinMarketCapApiOptions> coinMarketCapApiOptions)
    {
        _httpClient = httpClientFactory.CreateClient("CoinMarketCapClient");
        _coinMarketCapApiOptions = coinMarketCapApiOptions.Value;
        _configuration = configuration;
    }

    public async Task<ApiResponseModel> GetCoinMarketCapData(string symbol, string convertSymbols)
    {
        try
        {

            var requestUri = $"{_coinMarketCapApiOptions.Uri}v2/cryptocurrency/quotes/latest?symbol={symbol}&convert={convertSymbols}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("X-CMC_PRO_API_KEY", _coinMarketCapApiOptions.ApiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponseModel>(responseData);
            return result ?? new ApiResponseModel();
        }
        catch (HttpRequestException httpRequestException)
        {
            throw new ApplicationException("An error occurred while requesting data from CoinMarketCap.", httpRequestException);
        }
        catch (JsonException jsonException)
        {
            throw new ApplicationException("An error occurred while processing the response data.", jsonException);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred.", ex);
        }

    }
}
