namespace Okala.CryptoExchange.Application.Identities.Dtos;

public class GenerateJwtTokenResultDto
{
    public string JwtToken { get; set; }
    public DateTime ExpireDate { get; set; }

}
