using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Demo.EFCore.AzSQL.ConsoleApp.Models;
using System.Linq;
namespace Demo.EFCore.AzSQL.ConsoleApp
{
    class Program
    {

           private const string _prefix="";
        private const string _appsettings = "appsettings.json";
        private const string _hostsettings = "hostsettings.json";
        private const string _connectionKey="sqldbDatabase";
        static void  Main(string[] args)
        {
            var HostBuilder =  CreateHostBuilder(args).Build();
            var services = HostBuilder.Services.CreateScope();
             using (var context =services.ServiceProvider.GetService<ProductsContext>())
             {

               var result= context.Products
                                  .Include(p=>p.SalesOrderDetails)
                                  .Include(p=>p.ProductCategory);
                foreach (var product  in result)
                {
                    System.Console.WriteLine($"Product Name :{product.Name} " +
                                            $"and Price :{product.ListPrice} " +
                                            $"and Category Name:{product.ProductCategory.Name}"
                                          );
                }
            }
          HostBuilder.Run();
        }

        /// <summary>
        /// CreateHostBuilder for console application 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
         public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
             .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??"Development")
             .ConfigureHostConfiguration(configHost => 
             {
               //Configuring Host configuration
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile(_hostsettings, optional: true);
                    configHost.AddEnvironmentVariables();
                    configHost.AddCommandLine(args);
             })
             .ConfigureAppConfiguration((hostContext, configApp)=>{ 
               //Configuring App configuration
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(_appsettings, optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                                            optional: true);
                    configApp.AddEnvironmentVariables(prefix: _prefix);
                    if (hostContext.HostingEnvironment.IsDevelopment()){
                    configApp.AddUserSecrets<Program>();
                    }
                    configApp.AddCommandLine(args);
                    

                }
             )
             .ConfigureServices((HostContext,services)=> 
             {
               //configuring services 
                services.AddLogging();
                var databaseConnection=HostContext.Configuration.GetConnectionString(_connectionKey);
                services.AddDbContext<ProductsContext>(options =>{
                                                        options.UseSqlServer(databaseConnection);
                                                        options.EnableSensitiveDataLogging(); } );

             });
    }
}
