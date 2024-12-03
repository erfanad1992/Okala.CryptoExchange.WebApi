using System.Net;
using System.Text.Json;

namespace Okala.CryptoExchange.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var apiResponse = new ApiResponse<string>
            {
                Success = false,
                Data = null,
                Error = true,
                ErrorMessage = exception.Message
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";


            var result = JsonSerializer.Serialize(apiResponse);

            return context.Response.WriteAsync(result);
        }
    }
}
