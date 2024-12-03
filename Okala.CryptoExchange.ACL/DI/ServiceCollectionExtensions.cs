using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Okala.CryptoExchange.ACL.CoinMarketCap;
using Okala.CryptoExchange.ACL.CoinMarketCap.Services;
using Okala.CryptoExchange.Application.CoinMarketExchanges;

namespace Okala.CryptoExchange.ACL.DI;

public static class ServiceCollectionExtensions
{
    public static void AddACLServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<CoinMarketCapApiOptions>(configuration.GetSection("CoinMarketCapExchange"));
        services.AddHttpClient("CoinMarketCapClient", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<CoinMarketCapApiOptions>>().Value;

            client.BaseAddress = new Uri(options.Uri);
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", options.ApiKey);
        });

        services.AddScoped<ICoinMarketCapExchangeFromApiService, CoinMarketCapExchangeFromApiService>();
        services.AddScoped<ICryptoPriceService, CryptoPriceService>();
    }
}
