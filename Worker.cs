using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerServicesUsingEntityFramework.Models;
using WorkerServicesUsingEntityFramework.Services;

namespace WorkerServicesUsingEntityFramework
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        //We're adding the DbHelper class in order to enable us talk to the database.
        //Recall that the DbHelper class has some useful code that maps to SQL server.
        private readonly DbHelper dbHelper;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            //We're adding this to the Worker constructor so we have access to the database the instant the worker class is called.
            dbHelper = new DbHelper();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //The code below prints the text below every second.
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                List<User> users = dbHelper.GetAllUsers();
                {
                    //The first statement in the if block evaluates if there are users in the database.
                    //If there are no users in the database, a mock user detail is generated and inserted into the database
                    if(users.Count == 0)
                    {
                        dbHelper.SeedData();
                    }
                    else
                    {
                        //This method is used to log the user data
                        PrintUserInfo(users);
                    }
                }

                //The "Task.Delay" is what delays the messages for a second.
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void PrintUserInfo(List<User> users)
        {
            //This maps to each user in the database and gets their data on a case by case basis.
            users?.ForEach(user =>
            {
                _logger.LogInformation($"User Info: Name: {user.Name} and Address: {user.Address}");
            });
        }
    }
}
