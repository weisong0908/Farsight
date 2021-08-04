using System;
using System.Collections.Generic;
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

                var allTickersInDb = (await holdingRepository.GetHoldings())
                    .Select(h => h.Ticker)
                    .Distinct()
                    .ToList();

                Task A = GetTickersDetails(allTickersInDb, stockService, stoppingToken);
                Task B = GetPreviousClosePrices(allTickersInDb, stockService, stoppingToken);

                Task.WaitAll(new Task[] { A, B }, stoppingToken);
            }

            _logger.Information($"{nameof(StockDataSetupBackgroundService)} completed...");
        }

        private async Task GetTickersDetails(IList<string> tickers, IStockService stockService, CancellationToken stoppingToken, int callLimit = 5)
        {

            for (int i = 0; i < tickers.Count(); i++)
            {
                if (i % callLimit == 0 && i != 0)
                    await Task.Delay(70000, stoppingToken);

                _logger.Information("Get ticker details for {@ticker}. Call number {@index} ", tickers[i], i + 1);
                await stockService.GetTickerDetails(tickers[i]);
            }

        }

        private async Task GetPreviousClosePrices(IList<string> tickers, IStockService stockService, CancellationToken stoppingToken, int callLimit = 5)
        {
            for (int i = 0; i < tickers.Count(); i++)
            {
                if (i % callLimit == 0 && i != 0)
                    await Task.Delay(70000, stoppingToken);

                _logger.Information("Get ticker previous close price for {@ticker}. Call number {@index} ", tickers[i], i + 1);
                await stockService.GetPreviousClosePrice(tickers[i]);
            }
        }
    }
}