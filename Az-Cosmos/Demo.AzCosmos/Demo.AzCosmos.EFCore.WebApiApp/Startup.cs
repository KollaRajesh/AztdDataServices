using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.AzCosmos.EFCore.WebApiApp.Config;
using Demo.AzCosmos.EFCore.WebApiApp.Models;
using Demo.AzCosmos.EFCore.WebApiApp.Repositories.Infrastructure ;
using Demo.AzCosmos.EFCore.WebApiApp.Repositories;
using Demo.AzCosmos.EFCore.WebApiApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Demo.AzCosmos.EFCore.WebApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
                   Log.Logger = new LoggerConfiguration()
                                 .ReadFrom.Configuration(configuration)
                                 .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var colleageCosmosDBSettings=Configuration.GetSection(CollegeCosmosDBSettings.Settings).Get<CollegeCosmosDBSettings>();
                services.AddSingleton<CollegeCosmosDBSettings>(x=>colleageCosmosDBSettings );
                services.AddDbContext<CollegeDbContext>(options =>
                options.UseCosmos(colleageCosmosDBSettings.ServiceEndpoint,
                                  colleageCosmosDBSettings.AccountKey,
                                  colleageCosmosDBSettings.DatabaseId));


            services.AddTransient<IRepositoryAsync<College>,CollegeEFRepositoryAsync>();
            services.AddTransient<IRepository<College>,CollegeEFRepository>();

             services.AddTransient<ICollegeServiceAsync,CollegeServiceAsync>();
             services.AddTransient<ICollegeService,CollegeService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo.AzCosmos.EFCore.WebApiApp", Version = "v1" });
            });

            services.AddApiVersioning(apiVerConfig =>
                                        {
                                            apiVerConfig.AssumeDefaultVersionWhenUnspecified = true;
                                            apiVerConfig.DefaultApiVersion = new ApiVersion(new DateTime(2020, 12, 22));
                                        });

            services.AddHealthChecks()
                    .AddDbContextCheck<CollegeDbContext>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo.AzCosmos.EFCore.WebApiApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
