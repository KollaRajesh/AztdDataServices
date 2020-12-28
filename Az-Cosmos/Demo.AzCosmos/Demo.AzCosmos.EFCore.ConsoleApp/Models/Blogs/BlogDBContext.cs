
using System;
using Demo.AzCosmos.EFCore.ConsoleApp.Config;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class BlogDBContext : DbContext 
    {

        public BlogDBContext()
        {
            
        }
        public IServiceProvider ServiceProvider { get; }
        
        public BlogDBContext(DbContextOptions<BlogDBContext> options,IServiceProvider serviceProvider):base(options)
        {
            this.ServiceProvider = serviceProvider;

        }
     public  DbSet<Blog> Blogs {get;set;}
          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                var cosmosSettings = ServiceProvider.GetService<BlogsCosmosDBSettings>();
                optionsBuilder.UseCosmos(cosmosSettings.ServiceEndpoint,
                                         cosmosSettings.AccountKey,
                                         cosmosSettings.DatabaseId
                                        //  ,
                                        //  options =>
                                        //            {
                                        //             options.ConnectionMode(ConnectionMode.Gateway);
                                        //             options.WebProxy(new System.Net.WebProxy());
                                        //             options.LimitToEndpoint();
                                        //             options.Region(Regions.EastUS);
                                        //             options.GatewayModeMaxConnectionLimit(24);
                                        //             options.MaxRequestsPerTcpConnection(8);
                                        //             options.MaxTcpConnectionsPerEndpoint(16);
                                        //             options.IdleTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                                        //             options.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                                        //             options.RequestTimeout(TimeSpan.FromMinutes(1));
                                        //              }
                                         
                                         );
            }
        }


       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
         modelBuilder.HasDefaultContainer("Blogs");
          modelBuilder.Entity<Blog>()
                //.ToContainer("Blogs")
                //.HasPartitionKey(b=>b.Type)
                .OwnsMany(x=>x.Posts);
                // modelBuilder.Entity<Post>()
                //.ToContainer("Posts")
                //.HasPartitionKey(p=>p.Type);

       }

     
    }
}