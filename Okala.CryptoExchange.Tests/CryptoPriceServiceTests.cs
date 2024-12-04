using Moq;
using Okala.CryptoExchange.ACL.CoinMarketCap.Services;
using Okala.CryptoExchange.Application.CoinMarketExchanges;
using Okala.CryptoExchange.Application.CoinMarketExchanges.ApiResponseDtos;
using Xunit;

namespace Okala.CryptoExchange.Tests;

public class CryptoPriceServiceTests
{
    private readonly Mock<ICoinMarketCapExchangeFromApiService> _mockExchangeService;
    private readonly CryptoPriceService _cryptoPriceService;

    public CryptoPriceServiceTests()
    {
        _mockExchangeService = new Mock<ICoinMarketCapExchangeFromApiService>();
        _cryptoPriceService = new CryptoPriceService(_mockExchangeService.Object);
    }
    [Fact]
    public async Task GetCryptoPriceQuote_ValidSymbol_ReturnsCryptoPriceResponseDto()
    {
        // Arrange  
        var symbol = "BTC";
        var convertSymbols = "USD,EUR,BRL,GBP,AUD";
        var response = new ApiResponseModel
        {
            Status = new Status { ErrorCode = 0 },
            Data = new Dictionary<string, List<ApiCryptocurrency>>
                {
                    { symbol, new List<ApiCryptocurrency>
                        {
                            new ApiCryptocurrency
                            {
                                Name = "Bitcoin",
                                Quote = new Quote
                                {
                                    USD = new QuoteDetail { Price = 50000 },
                                    EUR = new QuoteDetail { Price = 46000 },
                                    BRL = new QuoteDetail { Price = 250000 },
                                    GBP = new QuoteDetail { Price = 40000 },
                                    AUD = new QuoteDetail { Price = 68000 }
                                }
                            }
                        }
                    }
                }
        };

        _mockExchangeService
            .Setup(s => s.GetCoinMarketCapData(symbol, convertSymbols))
            .ReturnsAsync(response);

        // Act  
        var result = await _cryptoPriceService.GetCryptoPriceQuote(symbol, convertSymbols);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal("Bitcoin", result.CryptocurrencyName);
        Assert.Equal(50000, result.PriceInUSD);
        Assert.Equal(46000, result.PriceInEUR);
        Assert.Equal(250000, result.PriceInBRL);
        Assert.Equal(40000, result.PriceInGBP);
        Assert.Equal(68000, result.PriceInAUD);
    }

    [Fact]
    public async Task GetCryptoPriceQuote_InvalidSymbol_ThrowsException()
    {
        // Arrange  
        var symbol = "INVALID_SYMBOL";
        var convertSymbols = "USD,EUR";

        var response = new ApiResponseModel
        {
            Status = new Status { ErrorCode = 0 },
            Data = new Dictionary<string, List<ApiCryptocurrency>>()
        };

        _mockExchangeService
            .Setup(s => s.GetCoinMarketCapData(symbol, convertSymbols))
            .ReturnsAsync(response);

        // Act  
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            _cryptoPriceService.GetCryptoPriceQuote(symbol, convertSymbols));
        //Assert 
        Assert.Equal("Cryptocurrency not found", ex.Message);
    }

    [Fact]
    public async Task GetCryptoPriceQuote_ApiServiceThrowsException_RethrowsException()
    {
        // Arrange  
        var symbol = "BTC";
        var convertSymbols = "USD,EUR";

        _mockExchangeService
            .Setup(s => s.GetCoinMarketCapData(symbol, convertSymbols))
            .ThrowsAsync(new Exception("API Error"));

        // Act 
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            _cryptoPriceService.GetCryptoPriceQuote(symbol, convertSymbols));
        //Assert  
        Assert.Equal("API Error", ex.Message);
    }
}