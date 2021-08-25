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
            //We split the build and the run method so the host is built, a database is created if it currently doesn't exist afterwhich we run the host.
            IHost host = CreateHostBuilder(args).Build();

            CreateDatabaseIfNotExist(host);

            host.Run();
            
        }

        //This is where we define the method that is called if a database doesn't exist when the program is 
        private static void CreateDatabaseIfNotExist(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    //The method below ensures that the database is created
                    context.Database.EnsureCreated();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    //This is where we leverage the ConnectionString property in the AppSettings class
                    AppSettings.Configuration = configuration;

                    //Here is where we declare the name of the connection string in the "appsetting.json" file in order to plug this into the method that maps the SQL server to the path it needs to access to get the table
                    AppSettings.ConnectionString = configuration.GetConnectionString("DefaultConn");

                    //We use the variable to map to the AppDbContext which has necessary information for talking to the database.
                    var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

                    //The line of code below is what we use to map to Sql by plugging in the name of the connection string in "appsettings.json".
                    optionBuilder.UseSqlServer(AppSettings.ConnectionString);

                    //Within the same type of request fronm AppDbContext, we get a new instance of AppDbContext.
                    services.AddScoped<AppDbContext>(d => new AppDbContext(optionBuilder.Options));

                    services.AddHostedService<Worker>();
                });
    }
}
