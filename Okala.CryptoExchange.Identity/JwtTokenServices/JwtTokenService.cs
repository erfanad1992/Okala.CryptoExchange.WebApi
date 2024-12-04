using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Okala.CryptoExchange.Application.Identities;
using Okala.CryptoExchange.Application.Identities.Dtos;

namespace Okala.CryptoExchange.Identity.JwtTokenServices;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public GenerateJwtTokenResultDto GenerateJwtToken()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:UserName"]!.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var token = new JwtSecurityToken
            (
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );
        var writedToken= new JwtSecurityTokenHandler().WriteToken(token);

        return new GenerateJwtTokenResultDto() 
        {
            JwtToken= writedToken,
            ExpireDate=token.ValidTo
        };

    }
}
