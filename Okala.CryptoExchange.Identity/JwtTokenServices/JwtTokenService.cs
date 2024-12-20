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
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // Encryption key and credentials
        var encryptionKey = Encoding.UTF8.GetBytes(_configuration["Jwt:EncriptKey"]!);
        var encryptingCredentials = new EncryptingCredentials(
            new SymmetricSecurityKey(encryptionKey),
            SecurityAlgorithms.Aes128KW,
            SecurityAlgorithms.Aes128CbcHmacSha256);

        // Claims
        var claims = new[] {
        //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:UserName"]!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

        // Create JWT Security Token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Issuer = _configuration["Jwt:Issuer"],        // Set Issuer
            Audience = _configuration["Jwt:Audience"],
        };

        // Create encrypted JWT (JWE)
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(securityToken);
        // Return token details
        return new GenerateJwtTokenResultDto
        {
            JwtToken = encryptedToken,
            ExpireDate = securityToken.ValidTo
        };

    }

    public Task<ClaimsPrincipal> ValidateToken(string token)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:EncriptKey"]!));

        var tokenHandler = new JwtSecurityTokenHandler();

        // Validation parameters
        var validationParameters = new TokenValidationParameters
        {

            // Validation for signing credentials
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,

            // Validation for encryption credentials
            TokenDecryptionKey = encryptionKey,

            // Validate issuer and audience
            ValidateIssuer = true,
            ValidIssuer = _configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = _configuration["Jwt:Audience"],

            // Lifetime validation
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Adjust clock skew for precise expiration checks
        };

        try
        {
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return Task.FromResult(principal); // Token is valid
        }
        catch (SecurityTokenException ex)
        {
            throw new Exception($"Token validation failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred during token validation.", ex);
        }
    }
}
