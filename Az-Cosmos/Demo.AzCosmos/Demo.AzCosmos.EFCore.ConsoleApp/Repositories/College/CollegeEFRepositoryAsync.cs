using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using Demo.AzCosmos.EFCore.ConsoleApp.Models;
using Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure;
using Demo.AzCosmos.EFCore.ConsoleApp.Repositories.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Repositories
{
    public class CollegeEFRepositoryAsync : IRepositoryAsync<College>
    {

        public IServiceProvider ServiceProvider { get; }
        public CollegeEFRepositoryAsync(IServiceProvider serviceProvider) 
        {
            this.ServiceProvider = serviceProvider;
        }
        public async Task<College> GetByIdAsync(int entityId)
        {
             using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider
                                   .GetRequiredService<CollegeDbContext>();

                return await context
                             .Colleges
                             .Where(x=>x.Id ==entityId)
                             .FirstOrDefaultAsync();
                
            }
        }

        public async Task<College> GetByIdAsync(int entityId, string partitionKey)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();

                return await context
                             .Colleges
                             .Where(x=>x.Id ==entityId && x.PartitionId ==partitionKey)
                             .FirstOrDefaultAsync();
                
            }
        }

        public async Task<ICollection<College>> GetAllAsync()
        {
          using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();

                return await context
                             .Colleges
                             .ToListAsync();
            }
        }

        public async Task<bool> IsExists(int entityId)
        {
               return await GetByIdAsync(entityId) !=null;
        }

        public async Task<bool> IsExists(int entityId, string partitionKey)
        {
                 return await GetByIdAsync(entityId,partitionKey) !=null;
        }
        public async  Task<int> SaveChangesAsync(College Entity)
        {
             using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                 if (! await IsExists(Entity.Id, Entity.PartitionId))
                 {
                     context.Add(Entity);
                   return await context.SaveChangesAsync();
                 }else 
                 {
                    context.UpdateAfterDetachLocalIfAny(Entity
                                ,Entity.Id 
                                ,Entity.PartitionId);

                    return await context.SaveChangesAsync();

                 }
            }
        }
         public async  Task<List<int>> SaveChangesAsync(ICollection<College> Entities)
        {
             var results=new List<int>();

             foreach (var Entity in Entities)
             {
                   results.Add(await  SaveChangesAsync(Entity));
             }
           
              return results;
        }

        public  async Task CreateDatabaseIfNotExistsAsync()
        {
               try
               {
                   using (var scope = ServiceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                        await context.Database.EnsureCreatedAsync();
                    }
               }
                catch(CosmosException) 
               {
                   System.Console.WriteLine($"Database for {nameof(CollegeDbContext)} is already exists ");
               }
        }

        public  async Task DeleteDatabaseAsync()
        {
               try
               {
                   using (var scope = ServiceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                        await  context.Database.EnsureDeletedAsync();
                    }
               }
                catch(CosmosException) 
               {
                   System.Console.WriteLine($"Database for {nameof(CollegeDbContext)} is not exist");
               }
        }

        public async Task DeleteEntityAsync(College Entity)
        {
             using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                 if (! await IsExists(Entity.Id, Entity.PartitionId))
                 {
                    context.DeleteAfterDetachLocalIfAny(Entity
                                                        ,Entity.Id 
                                                        ,Entity.PartitionId);


                 }
            }
        }
    }
}