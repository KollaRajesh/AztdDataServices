using System;
using Microsoft.Extensions.Hosting;
using Demo.EFCore.AzSQL.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Demo.EFCore.AzSQL
{
    public  class Program
    {
        private const string _prefix="";
        private const string _appsettings = "appsettings.json";
        private const string _hostsettings = "hostsettings.json";
        public static async Task  Main(string[] args)
        {
                
          var HostBuilder =  CreateHostBuilder(args).Build();
          var services= HostBuilder.Services.CreateScope();

          using (var context= services.ServiceProvider.GetService<sqldbaztdContext>())
          {
            foreach (var product in context.Products)
            {
                Console.WriteLine("Product Name : {0} and price :{1}" , product.Name, product.ListPrice);
            }

          }

        await HostBuilder.RunAsync();

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
                var database=HostContext.Configuration.GetConnectionString("sqldbDatabase");
                services.AddDbContext<sqldbaztdContext>(options =>
                options.UseSqlServer(database));

             });
 
    }
}
