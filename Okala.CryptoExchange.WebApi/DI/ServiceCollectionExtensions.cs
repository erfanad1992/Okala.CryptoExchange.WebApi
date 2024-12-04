using Microsoft.OpenApi.Models;
using Okala.CryptoExchange.ACL.DI;
using Okala.CryptoExchange.Identity.DI;

namespace Okala.CryptoExchange.WebApi.DI
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //----------- Infra -----------
            services.AddACLServices(configuration);
            services.AddIdentityServices(configuration);

            //------------ Endpoint -------

            services.AddControllers();
            services.AddAuthentication();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                c =>
                {
                    // Add JWT authentication lock
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                {
                 new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                 new string[] {}
                }
                    });
                });

            return services;

        }
    }
}
