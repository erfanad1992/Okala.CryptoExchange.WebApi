using Microsoft.AspNetCore.Mvc;
using Okala.CryptoExchange.Application.Identities;
using Okala.CryptoExchange.Application.Identities.Dtos;
using Okala.CryptoExchange.WebApi.Middlewares;

namespace Okala.CryptoExchange.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public IdentityController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("createToken")]
        public async Task<IActionResult> CreateToken()
        {
            var token = _jwtTokenService.GenerateJwtToken();
            return Ok(ApiResponse<GenerateJwtTokenResultDto>.SuccessResponse(token));

        }

    }
}
