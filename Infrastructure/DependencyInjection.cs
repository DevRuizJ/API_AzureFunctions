using Application.Common;
using Application.Interface;
using Infrastructure.Repository;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFilter(level => true);
            });

            var config = (IConfiguration)builder.Services.First(d => d.ServiceType == typeof(IConfiguration)).ImplementationInstance;

            builder.Services.AddSingleton((s) =>
            {
                // Provide your own primary connection key
                CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder(Constants.COSMOS_DB_CONNECTION_STRING);

                return cosmosClientBuilder.WithConnectionModeDirect()
                    .WithApplicationRegion("East US 2")
                    .WithBulkExecution(true)
                    .Build();
            });


            //Repository
            services.AddScoped<IConsentRepo, ConsentRepo>();

            return services;
        }
    }
}