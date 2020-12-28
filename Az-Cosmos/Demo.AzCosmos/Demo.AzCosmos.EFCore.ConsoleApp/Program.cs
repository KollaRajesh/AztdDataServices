using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Demo.AzCosmos.EFCore.ConsoleApp.Config;
using Demo.AzCosmos.EFCore.ConsoleApp.Models;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Linq;
using Demo.AzCosmos.EFCore.ConsoleApp.Repositories;
using Demo.AzCosmos.EFCore.ConsoleApp.Services;
using  Demo.AzCosmos.EFCore.ConsoleApp.Repositories.Infrastructure;
// using Microsoft.EntityFrameworkCore;
// using System.Threading.Tasks;

namespace Demo.AzCosmos.EFCore.ConsoleApp
{
    class Program
    {


        private const string _prefix="";
        private const string _appsettings = "appsettings.json";
        private const string _hostsettings = "hostsettings.json";

        //    // The Cosmos client instance
        // private CosmosClient cosmosClient;

        // // // The database we will create
        // private Database database;

        // // // The container we will create.
        // private Container container;
        private static IServiceProvider ServiceProvider ;


        //static async Task Main(string[] args)
        static void Main(string[] args)
        {
            var HostBuilder = CreateHostBuilder(args).Build();
            var services = HostBuilder.Services.CreateScope();
            ServiceProvider=services.ServiceProvider;

            var blogservice=ServiceProvider.GetService<BlogService>();
               blogservice.CreateSampleBlogs();
               blogservice.WriteItemsToConsole();
                  
            var collegeService = ServiceProvider.GetService<CollegeService>();
            collegeService.CreateSampleColleges();
            collegeService.WriteItemsToConsole();
            collegeService.UpdateAddressforColleges();
            collegeService.WriteItemsToConsole();
            collegeService.UpdateDepartmentsforColleges();
            collegeService.WriteItemsToConsole();

             Console.Read();
         
           // HostBuilder.Run();
            //await HostBuilder.RunAsync();

        }



        /// <summary>
        /// CreateHostBuilder for console application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
             .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??"Production")
             .ConfigureHostConfiguration(configHost =>
             {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile(_hostsettings, optional: true);
                    configHost.AddEnvironmentVariables();
                    configHost.AddCommandLine(args);
             })
             .ConfigureAppConfiguration((hostContext, configApp)=>{
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(_appsettings, optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                                            optional: true);
                    configApp.AddEnvironmentVariables(prefix: _prefix);

                   if (hostContext.HostingEnvironment.IsDevelopment())
                   {
                     configApp.AddUserSecrets<Program>();
                    }
                    configApp.AddCommandLine(args);


                }
             )
             .ConfigureServices((HostContext,services)=>
             {
                services.AddLogging();
                
                var colleageCosmosDBSettings=HostContext.Configuration.GetSection(CollegeCosmosDBSettings.Settings).Get<CollegeCosmosDBSettings>();
                services.AddSingleton<CollegeCosmosDBSettings>(x=>colleageCosmosDBSettings );
                
                 var cosmosClient=new CosmosClient(colleageCosmosDBSettings.ServiceEndpoint
                                                , colleageCosmosDBSettings.AccountKey
                                                , new CosmosClientOptions()
                                                { ApplicationName = "Demo.AzCosmos.EFCore.ConsoleApp" });

            
                services.AddTransient<CosmosClient>(client=>cosmosClient );

                services.AddDbContext<CollegeDbContext>(options =>
                options.UseCosmos(colleageCosmosDBSettings.ServiceEndpoint,
                                  colleageCosmosDBSettings.AccountKey,
                                  colleageCosmosDBSettings.DatabaseId));

               // if want to use cosmos client instead of EF , comment below line.
                services.AddTransient<IRepositoryAsync<College>, CollegeEFRepositoryAsync>();
                
                // if want to use cosmos client instead of EF , uncomment below line 
                //services.AddTransient<IRepositoryAsync<College>, CollegeCosmosClientRepositoryAsync>();
                services.AddTransient<CollegeServiceAsync>();
                

                services.AddTransient<IRepository<College>, CollegeEFRepository>();
                services.AddTransient<CollegeService>();


                var blogsCosmosDBSettings=HostContext.Configuration.GetSection(BlogsCosmosDBSettings.Settings).Get<BlogsCosmosDBSettings>();
                services.AddSingleton<BlogsCosmosDBSettings>(x=>blogsCosmosDBSettings );
                services.AddDbContext<BlogDBContext>(options =>
                    options.UseCosmos(blogsCosmosDBSettings.ServiceEndpoint,
                                    blogsCosmosDBSettings.AccountKey,
                                    blogsCosmosDBSettings.DatabaseId));
                services.AddTransient<IRepository<Blog>, BlogRepository>();
                services.AddTransient<BlogService>();

            });

    }
}
