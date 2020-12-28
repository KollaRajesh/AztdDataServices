using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.AzCosmos.EFCore.WebApiApp.Config;
using Demo.AzCosmos.EFCore.WebApiApp.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;
using Demo.AzCosmos.EFCore.WebApiApp.Repositories.Infrastructure;

namespace Demo.AzCosmos.EFCore.WebApiApp.Repositories
{
    public class CollegeCosmosClientRepositoryAsync : IRepositoryAsync<College>
    {
        private readonly CosmosClient cosmosClient;
        private Database database;
        public CollegeCosmosDBSettings Settings { get; }

        private Container container;
        public CollegeCosmosClientRepositoryAsync(CollegeCosmosDBSettings settings,CosmosClient cosmosClient)
        {
            this.Settings = settings;
            this.cosmosClient=cosmosClient;
            //   this.ServiceProvider = serviceProvider;
             if (cosmosClient ==null)
             {
               this.cosmosClient = new CosmosClient(settings.ServiceEndpoint
                                                , settings.AccountKey
                                                , new CosmosClientOptions()
                                                { ApplicationName = "Demo.AzCosmos.EFCore.ConsoleApp" });
             }
        }
        public async Task CreateDatabaseIfNotExistsAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(this.Settings.DatabaseId);
            Console.WriteLine($"Created Database: {this.database.Id}");
            await CreateContainerAsync("PartitionId");
        }

        private async Task CreateContainerAsync(string partitionKeyName)
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(this.Settings.ContainerId, 
                                                                                  $"/{partitionKeyName}", 400);
            Console.WriteLine($"Created Container: {this.container.Id}");
        }

        public async Task DeleteDatabaseAsync()
        {
            DatabaseResponse databaseResourceResponse = await this.database.DeleteAsync();

            Console.WriteLine($"Deleted Database: {this.Settings.DatabaseId}");

            //Dispose of CosmosClient
            this.cosmosClient.Dispose();
        }

        public async Task<College> GetByIdAsync(int entityId)
        {
            var sqlQueryText = $"SELECT * FROM c WHERE c.Id = @collegeId";

           Console.WriteLine("Running query: {sqlQueryText}\n" );

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            queryDefinition.WithParameter("@collegeId", entityId);
            FeedIterator<College> queryResultSetIterator = this.container
                                                        .GetItemQueryIterator<College>(queryDefinition);

            List<College> colleges = new List<College>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<College> currentResultSet = await queryResultSetIterator
                                                                .ReadNextAsync();
                foreach (College college in currentResultSet)
                {
                    colleges.Add(college);
                    Console.WriteLine($"\tRead {college}\n" );
                }
            }
            return colleges.FirstOrDefault();
        }

        public async Task<College> GetByIdAsync(int entityId, string partitionKey)
        {
           ItemResponse<College> entityResponse;
           College result=null;
            try
            {
                // Read the item to see if it exists.  
                 entityResponse = await this.container.ReadItemAsync<College>(entityId.ToString(),
                new PartitionKey(partitionKey));
                 Console.WriteLine($"Item in database with id: {entityResponse.Resource.Id}  exists");
            }
            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                   entityResponse =  null;
            }
             
             if(entityResponse !=null)
             {
               result = entityResponse.Resource;
             }
            return result;
        }

        public async Task<College> GetByNameAsync(string collegeName)
        {
               var sqlQueryText = $"SELECT * FROM c WHERE c.Name = @collegeName";

           Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            queryDefinition.WithParameter("@collegeName", collegeName);
            FeedIterator<College> queryResultSetIterator = this.container
                                                        .GetItemQueryIterator<College>(queryDefinition);

            List<College> colleges = new List<College>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<College> currentResultSet = await queryResultSetIterator
                                                                .ReadNextAsync();
                foreach (College college in currentResultSet)
                {
                    colleges.Add(college);
                    Console.WriteLine($"\tRead {college}\n" );
                }
            }
            return colleges.FirstOrDefault();
        }

        public async Task<ICollection<College>> GetAllAsync()
        {
              var sqlQueryText = $"SELECT * FROM c ";

            Console.WriteLine($"Running query: {sqlQueryText}");

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<College> queryResultSetIterator = this.container
                                                            .GetItemQueryIterator<College>(queryDefinition);

            List<College> colleges = new List<College>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<College> currentResultSet = await queryResultSetIterator
                                                                .ReadNextAsync();
                foreach (College college in currentResultSet)
                {
                    colleges.Add(college);
                    Console.WriteLine($"\tRead {college}");
                }
            }
            return colleges;
        }

        public async Task<bool> IsExists(int entityId)
        {
             return (await GetByIdAsync(entityId)) !=null ;
        }

        public async Task<bool> IsExists(int entityId, string partitionKey)
        {
            return (await GetByIdAsync(entityId,partitionKey)) !=null ;
        }

        public async Task<int> SaveChangesAsync(College entity)
        {
            int result=0;
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<College> entityResponse = await this
                                                                .container
                                                                .ReadItemAsync<College>(entity.Id.ToString(),
                new PartitionKey(entity.PartitionId.ToString()));
                Console.WriteLine($"Item in database with id: { entityResponse.Resource.Id} already exists");


                 var itemBody = entityResponse.Resource;

                // replace the item with the updated content
                      entityResponse = await this.container
                                            .ReplaceItemAsync<College>(itemBody,
                                             itemBody.Id.ToString(),
                                              new PartitionKey(itemBody.PartitionId));
                Console.WriteLine($"Updated College [{itemBody.PartitionId},{itemBody.Id}].\n \t Body is now: {entityResponse.Resource}" );
            }
            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<College> response = await this.container
                                                        .CreateItemAsync<College>(entity,
                new PartitionKey(entity.PartitionId.ToString()));
                   result= response.Resource.Id;
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                 Console.WriteLine($"Created item in database with id: {entity.Id} Operation consumed {response.RequestCharge} RUs." );
            }
            return result;
        }

        public async Task<List<int>> SaveChangesAsync(ICollection<College> Entities)
        {
             var results=new List<int>();
             foreach (var Entity in Entities)
             {
                   results.Add( await SaveChangesAsync(Entity));
             }
              return results;
        }
        

        public  async Task DeleteEntityAsync(College entity)
        {

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<College> response = await this.container.DeleteItemAsync<College>
                                                                        (entity.Id.ToString(),
                                                                        new PartitionKey(entity.PartitionId));
            Console.WriteLine($"Deleted entity [{entity.PartitionId},{entity.Id}]");
        }

       
    }
}