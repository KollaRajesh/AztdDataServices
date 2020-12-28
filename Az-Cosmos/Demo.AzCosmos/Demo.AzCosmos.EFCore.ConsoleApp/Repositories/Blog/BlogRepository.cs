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
    public class BlogRepository: IRepository<Blog>
    {
          public IServiceProvider ServiceProvider { get; }
          public BlogRepository(IServiceProvider serviceProvider) 
        {
            this.ServiceProvider = serviceProvider;
        }
         public bool  IsExists(int EntryId)
         {
            return  GetById(EntryId) !=null;

         }
       public  bool IsExists(int EntryId, string partitionKey)
       {
          var entity= GetById(EntryId,partitionKey) ;
          return  entity !=null ;

       }

        public ICollection<Blog> GetAll()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BlogDBContext>();
                return  context.Blogs
                             .ToList();
            }

        }
        public  Blog GetById(int EntryId)
        {
           using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider
                                   .GetRequiredService<BlogDBContext>();
                return  context
                             .Blogs
                             .Where(x=>x.Id ==EntryId)
                             .FirstOrDefault();
                
            }
        }
        public Blog GetById(int EntryId, string partitionKey)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BlogDBContext>();

                return  context
                             .Blogs
                             .Where(x=>x.Id ==EntryId && x.PartitionId ==partitionKey)
                             .FirstOrDefault();
                
            }

        }
        public int SaveChanges(Blog Entity)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BlogDBContext>();
                 if (!IsExists(Entity.Id, Entity.PartitionId))
                 {
                     context.Add(Entity);
                     return context.SaveChanges();
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
        public IList<int> SaveChanges(ICollection<Blog> Entities)
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
                        var context = scope.ServiceProvider.GetRequiredService<BlogDBContext>();
                         context.Database.EnsureCreated();
                    }
               }
                catch(CosmosException) 
               {
                   System.Console.WriteLine($"Database for {nameof(CollegeDbContext)} is already exists ");
               }
        }
        public void  DeleteDatabase()
        {
              try
               {
                    using (var scope = ServiceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<BlogDBContext>();
                        context.Database.EnsureDeleted();
                    }
               }
                catch(CosmosException) 
               {
                   System.Console.WriteLine($"Database for {nameof(CollegeDbContext)} is not exist");
               }
        }

        public void DeleteEntity(Blog entity)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                 if (! IsExists(entity.Id, entity.PartitionId))
                 {
                    context.DeleteAfterDetachLocalIfAny(entity
                                                        ,entity.Id 
                                                        ,entity.PartitionId);


                 }
            }
        }
    }
}