using System.Text.Json.Serialization;

namespace Okala.CryptoExchange.Application.CoinMarketExchanges.ApiResponseDtos
{
    public class ApiResponseModel
    {
        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, List<ApiCryptocurrency>> Data { get; set; }
    }
}
