using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Demo.AzCosmos.EFCore.ConsoleApp.Config;
using System.Linq;
using System.Collections.Generic;
using Demo.AzCosmos.EFCore.ConsoleApp.Models;
using Demo.AzCosmos.EFCore.ConsoleApp.Repositories.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Services
{
    public class BlogService{
        
        private readonly IRepository<Blog> _repository ;

        public BlogService(IRepository<Blog> repository){
            _repository = repository;
        }
         public void  DeleteCollegeDatabaseIfExists(){
            _repository.DeleteDatabase();
        }

        /// <summary>
        ///  This is for demo purpose
        /// </summary>
        /// <returns></returns>
        public   void  CreateSampleBlogs(){
                //_repository.DeleteDatabase();
          _repository.CreateDatabaseIfNotExists();

          var blog = CreateBlogForAzureDevOps();
              _repository.SaveChanges(blog);
 
              blog = CreateBlogForAzureFunctions();
             _repository.SaveChanges(blog);

              blog = CreateBlogForAzureDatabases();
             _repository.SaveChanges(blog);
         }
 
         public List<Blog> GetBlogs()
         {
            return  _repository.GetAll().ToList();
         }


public void WriteItemsToConsole ()
{

    var blogs= GetBlogs();
  blogs.ToList().ForEach( blog =>
                 {

                  Console.WriteLine($" Blog details with name : {blog.BlogTitle}  and Blog URL : {blog.BlogUrl}");
                  blog.Posts
                      .ToList()
                      .ForEach( 
                            post => 
                                Console.WriteLine($" post {post.Title} exists in blog  : {blog.BlogTitle} ")
                                );
                });

}

        private static Blog CreateBlogForAzureDatabases()
        {
            var blog = new Blog { Id = 3, BlogUrl = "https://blogger.AzureDatabases.com", PartitionId = "database", BlogTitle = "Azure Databases" };
            blog.Posts = new HashSet<Post>();
            blog.Posts.Add(new Post { Id = "3.1", Title = "Azure SQL Databases- Overview Sync Groups.", PartitionId = "database" });
            blog.Posts.Add(new Post { Id = "3.2", Title = "How to use Azure Cosmos DB Emulator & Azure Cosmos DB Data Migration Tool.", PartitionId = "cosmos" });
            blog.Posts.Add(new Post { Id = "3.3", Title = "Suporting and fully Managing third parties Databases in Azure.", PartitionId = "database" });
            return blog;
        }

        private static Blog CreateBlogForAzureFunctions()
        {
            var blog = new Blog { Id = 2, BlogUrl = "https://blogger.Azurefunctions.com", PartitionId = "functions", BlogTitle = "Azure Functions" };
            blog.Posts = new HashSet<Post>();
            blog.Posts.Add(new Post { Id = "2.1", Title = "Azure Functions used as Web API Service", PartitionId = "Web API" });
            blog.Posts.Add(new Post { Id = "2.2", Title = "Azure Functions integraion with Cosmos DB", PartitionId = "Azure Functions" });
            blog.Posts.Add(new Post { Id = "2.3", Title = "Azure Functions with VS code using Azure Core Function Toools", PartitionId = "Function tools" });
            return blog;
        }

        private static Blog CreateBlogForAzureDevOps()
        {
            var blog = new Blog { Id = 1, BlogUrl = "https://blogger.azuredevops.com", PartitionId = "devops", BlogTitle = "Azure DevOps" };
            blog.Posts = new HashSet<Post>();
            blog.Posts.Add(new Post { Id = "1.1", Title = "Azure Arc with Azure Kubernetes", PartitionId = "Kubernetes" });
            blog.Posts.Add(new Post { Id = "1.2", Title = "Azure Functions deployment using Azure pipelines", PartitionId = "Azure Functions" });
            blog.Posts.Add(new Post { Id = "1.3", Title = "Azure Devops integration with Github", PartitionId = "GitHub Integration" });
            return blog;
        }
       


    }
}
