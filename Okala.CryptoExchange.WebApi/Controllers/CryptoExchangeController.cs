using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okala.CryptoExchange.Application.CoinMarketExchanges;
using Okala.CryptoExchange.WebApi.Middlewares;

namespace Okala.CryptoExchange.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoExChangeController : ControllerBase
    {
        private readonly ICryptoPriceService _cryptoPriceService;

        public CryptoExChangeController(ICryptoPriceService cryptoPriceService)
        {
            _cryptoPriceService = cryptoPriceService;
        }

        // GET api/cryptoexchange/quote?symbol=BTC&convertSymbols=USD,EUR,BRL,GBP,AUD
        [HttpGet("quote")]
        [Authorize]
        public async Task<IActionResult> GetCoinMarketCapQuote([FromQuery] string symbol, [FromQuery] string convertSymbols)
        {
            var response = await _cryptoPriceService.GetCryptoPriceQuote(symbol, convertSymbols);
            return Ok(ApiResponse<CryptoPriceResponseDto>.SuccessResponse(response));

        }
    }
}
