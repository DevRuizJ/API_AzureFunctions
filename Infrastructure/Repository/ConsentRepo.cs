using Application.Common;
using Application.Entity;
using Application.Interface;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ConsentRepo : IConsentRepo
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private CosmosClient _cosmosClient;

        private Database _database;
        private Container _container;

        public ConsentRepo(ILogger<ConsentRepo> logger, IConfiguration config, CosmosClient cosmosClient) 
        {
            _logger = logger;
            _config = config;
            _cosmosClient = cosmosClient;

            _database = _cosmosClient.GetDatabase(Constants.COSMOS_DB_DATABASE_NAME);
            _container = _database.GetContainer(Constants.COSMOS_DB_CONTAINER_NAME);
        }

        public async Task<string> Create(AcquiescenceEntity data)
        {
            string message = "No se realizó el registro";
            
            try 
            {
                ItemResponse<AcquiescenceEntity> acquiescence = await _container.CreateItemAsync(data);

                message = "Registro ok";

            }
            catch (Exception ex) 
            {
                message = ex.Message;
            }

            return message;
        }

        public async Task<AcquiescenceEntity> GetById(string id)
        {
            string message = "No se realizó el registro";

            try 
            {
                var acquiescence = await _container.ReadItemAsync<AcquiescenceEntity>(
                    id: id,
                    partitionKey: new PartitionKey("id")
                    );

                //acquiescence = _container.GetItemQueryIterator<AcquiescenceEntity>(new QueryDefinition("SELECT * FROM c where c.id = '" + id + "'"));
                //var result = acquiescence.;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return null;
        }

        public async Task<List<AcquiescenceEntity>> GetList()
        {
            List<AcquiescenceEntity> result = new List<AcquiescenceEntity>();

            try
            {
                var query = _container.GetItemQueryIterator<AcquiescenceEntity>(new QueryDefinition("Select * from c"));

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    result.AddRange(response);
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
