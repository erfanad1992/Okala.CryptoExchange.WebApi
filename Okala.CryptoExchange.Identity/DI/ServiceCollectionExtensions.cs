using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Okala.CryptoExchange.Application.Identities;
using Okala.CryptoExchange.Identity.JwtTokenServices;

namespace Okala.CryptoExchange.Identity.DI
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var encryptKey = configuration["JWT:EncriptKey"];

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                var encriptionKey = Encoding.UTF8.GetBytes(configuration["JWT:EncriptKey"]);

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience= configuration["Jwt:Audience"],
                    ValidIssuer= configuration["Jwt:Issuer"],
                    TokenDecryptionKey= new SymmetricSecurityKey(encriptionKey),
                };
            });

            services.AddSingleton<IJwtTokenService,JwtTokenService>();
            return services;
        }
    }
}

