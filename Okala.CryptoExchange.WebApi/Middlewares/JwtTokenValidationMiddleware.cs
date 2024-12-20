using Microsoft.IdentityModel.Tokens;
using Okala.CryptoExchange.Application.Identities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Okala.CryptoExchange.WebApi.Middlewares;

public class JwtTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IJwtTokenService _jwtTokenService;


    public JwtTokenValidationMiddleware(RequestDelegate next, IConfiguration configuration, IJwtTokenService jwtTokenService)
    {
        _next = next;
        _configuration = configuration;
        _jwtTokenService = jwtTokenService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var claimsPrincipal =await _jwtTokenService.ValidateToken(token);

                // Attach user information to HttpContext for later use
                context.User = claimsPrincipal;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid token", details = ex.Message });
                return; // Stop further middleware processing
            }
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
