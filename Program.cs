using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerServicesUsingEntityFramework.Services;

namespace WorkerServicesUsingEntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    //We use the variable to map to the AppDbContext which has necessary information for talking to the database.
                    var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
                    
                    //The line of code below is what we use to map to Sql by plugging in the name of the connection string in "appsettings.json".
                    optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConn"));

                    //Within the same type of request fronm AppDbContext, we get a new instance of AppDbContext.
                    services.AddScoped<AppDbContext>(d => new AppDbContext(optionBuilder.Options));

                    services.AddHostedService<Worker>();
                });
    }
}
