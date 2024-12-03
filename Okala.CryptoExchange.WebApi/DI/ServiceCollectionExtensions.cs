using Okala.CryptoExchange.EfPersistance.DI;
namespace Okala.CryptoExchange.WebApi.DI
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //----------- Infra -----------
            services.AddPersistenceEntityFrameworkServices(configuration);

            //------------ Endpoint -------

            services.AddControllers();
           
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;

        }
    }
}
