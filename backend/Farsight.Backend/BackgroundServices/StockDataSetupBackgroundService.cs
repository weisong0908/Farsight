using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Farsight.Backend.Persistence;
using Farsight.Backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Farsight.Backend.BackgroundServices
{
    public class StockDataSetupBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;

        public StockDataSetupBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"{nameof(StockDataSetupBackgroundService)} starting...");

            stoppingToken.Register(() =>
                _logger.Information($"{nameof(StockDataSetupBackgroundService)} stopping..."));

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
                var holdingRepository = scope.ServiceProvider.GetRequiredService<IHoldingRepository>();

                await stockService.GetTickers();

                var allTickers = (await holdingRepository.GetHoldings()).Select(h => h.Ticker).Distinct();
                foreach (var ticker in allTickers)
                    await stockService.GetTickerDetails(ticker);
            }

            _logger.Information($"{nameof(StockDataSetupBackgroundService)} completed...");
        }
    }
}