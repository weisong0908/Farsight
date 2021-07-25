using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Farsight.Backend.Services
{
    public class CustomService : BackgroundService
    {
        private readonly ILogger _logger;

        public CustomService(ILogger logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information("Custom Service: starting... ");

            stoppingToken.Register(() =>
                _logger.Information("Custom Service: stopping... "));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.Information("Custom Service: " + DateTime.Now.ToLongTimeString());

                await Task.Delay(5000, stoppingToken);
            }

            _logger.Information("Custom Service: stopping... ");
        }
    }
}