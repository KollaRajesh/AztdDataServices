
using System;
using Demo.AzCosmos.EFCore.ConsoleApp.Config;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext()
        {
        }

       public CollegeCosmosDBSettings Settings { get; }
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options,CollegeCosmosDBSettings settings )
            : base(options)
        {
              this.Settings = settings;

        }
        public DbSet<College> Colleges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseCosmos(Settings.ServiceEndpoint,
                                        Settings.AccountKey,
                                        Settings.DatabaseId
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
            // optionsBuilder.EnableDetailedErrors();
            // optionsBuilder.EnableSensitiveDataLogging();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
          modelBuilder.HasDefaultContainer(Settings.ContainerId);
          modelBuilder.Entity<College>()
                      .HasPartitionKey(c => c.PartitionId)
                      .OwnsOne(c => c.Address);
                     

          modelBuilder.Entity<College>()
                      .OwnsMany(c1=>c1.Departments)
                      .OwnsMany(d=>d.Courses);
     
         modelBuilder.Entity<College>()
                      .OwnsMany(c2=>c2.Resources);
                       

            
        }


      
    }
}