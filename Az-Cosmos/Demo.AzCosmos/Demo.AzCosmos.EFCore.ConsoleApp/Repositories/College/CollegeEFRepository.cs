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
    public class CollegeEFRepository : IRepository<College>
    {
          public IServiceProvider ServiceProvider { get; }
        public CollegeEFRepository(IServiceProvider serviceProvider) 
        {
            this.ServiceProvider = serviceProvider;
        }
       
        public College GetById(int entityId)
        {
             using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider
                                   .GetRequiredService<CollegeDbContext>();

                return  context
                             .Colleges
                             .Where(x=>x.Id ==entityId)
                             .FirstOrDefault();
                
            }
        }

        public College GetById(int entityId, string partitionKey)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();

                return  context
                             .Colleges
                             .Where(x=>x.Id ==entityId && x.PartitionId ==partitionKey)
                             .FirstOrDefault();
                
            }
        }

        public ICollection<College> GetAll()
        {
             using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();

                return  context.Colleges
                             .ToList();
            }
        }

        public bool IsExists(int entityId)
        {
             return  GetById(entityId) !=null;
        }

        public bool IsExists(int entityId, string partitionKey)
        {
             return  GetById(entityId,partitionKey) !=null;
        }

        public int SaveChanges(College Entity)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                 if (!  IsExists(Entity.Id, Entity.PartitionId))
                 {
                     context.Add(Entity);
                   return  context.SaveChanges();
                 }else 
                 {
                    context
                     .UpdateAfterDetachLocalIfAny(Entity
                                ,Entity.Id 
                                ,Entity.PartitionId);

                    return context.SaveChanges();

                 }
            }
        }

         public IList<int>  SaveChanges(ICollection<College> Entities)
        {
             var results=new List<int>();
             foreach (var Entity in Entities)
             {
                   results.Add(SaveChanges(Entity));
             }
              return results;
        }

        public void CreateDatabaseIfNotExists()
        {
               try
               {
                     using (var scope = ServiceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                         context.Database.EnsureCreated();
                    }
               }
                catch(CosmosException) 
               {
                   System.Console.WriteLine($"Database for {nameof(CollegeDbContext)} is already exists ");
               }
        }

        public void DeleteDatabase()
        {
               try
               {
                    using (var scope = ServiceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                        context.Database.EnsureDeleted();
                    }
               }
                catch(CosmosException) 
               {
                   System.Console.WriteLine($"Database for {nameof(CollegeDbContext)} is not exist");
               }
        }

         public void  DeleteEntity(College Entity)
        {
             using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                 if (! IsExists(Entity.Id, Entity.PartitionId))
                 {
                    context.DeleteAfterDetachLocalIfAny(Entity
                                                        ,Entity.Id 
                                                        ,Entity.PartitionId);


                 }
            }
        }
        
    }
}