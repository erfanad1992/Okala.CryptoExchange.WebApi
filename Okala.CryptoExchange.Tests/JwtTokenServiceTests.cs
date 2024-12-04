using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Okala.CryptoExchange.Identity.JwtTokenServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Okala.CryptoExchange.Tests
{
    public class JwtTokenServiceTests
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public JwtTokenServiceTests()
        {
            // Setup configuration with mock values  
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:Key", "YourSuperSecretKey12345" },
                { "Jwt:UserName", "erfandn" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtTokenService = new JwtTokenService(_configuration);
        }

        [Fact]
        public void GenerateJwtToken_ShouldReturnValidTokenResult()
        {
            // Act  
            var result = _jwtTokenService.GenerateJwtToken();

            // Assert  
            Assert.NotNull(result);
            Assert.NotNull(result.JwtToken);
            Assert.True(result.ExpireDate > DateTime.UtcNow);

            // Validate the token  
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            // Validate the token  
            var principal = tokenHandler.ValidateToken(result.JwtToken, tokenValidationParameters, out var validatedToken);
            Assert.NotNull(validatedToken);
            Assert.IsType<JwtSecurityToken>(validatedToken);

            // Check claims  
            var claimsIdentity = principal.Identity as ClaimsIdentity;
            Assert.NotNull(claimsIdentity);

        }
    }
}
