using Okala.CryptoExchange.Application.Identities.Dtos;
using System.Security.Claims;

namespace Okala.CryptoExchange.Application.Identities
{
    public interface IJwtTokenService
    {
        GenerateJwtTokenResultDto GenerateJwtToken();

        Task<ClaimsPrincipal> ValidateToken(string token);
    }
}
