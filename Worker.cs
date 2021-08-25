using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServicesUsingEntityFramework
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //The code below prints the text below every second.
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //The "Task.Delay" is what delays the messages for a second.
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
