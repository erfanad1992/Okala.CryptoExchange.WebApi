using Okala.CryptoExchange.Application.Identities.Dtos;

namespace Okala.CryptoExchange.Application.Identities
{
    public interface IJwtTokenService
    {
        GenerateJwtTokenResultDto GenerateJwtToken();
    }
}
