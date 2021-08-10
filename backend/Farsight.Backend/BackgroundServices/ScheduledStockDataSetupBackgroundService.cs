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
    public class ScheduledStockDataSetupBackgroundService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;
        private Timer _timer;
        private bool isTaskActive = false;

        public ScheduledStockDataSetupBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(ScheduledStockDataSetupBackgroundService)} starting...");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(125));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(ScheduledStockDataSetupBackgroundService)} stopping...");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            if (isTaskActive)
                return;

            isTaskActive = true;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
                var holdingRepository = scope.ServiceProvider.GetRequiredService<IHoldingRepository>();

                var allTickersInDb = (await holdingRepository.GetHoldings())
                    .Select(h => h.Ticker)
                    .Distinct()
                    .ToList();

                _logger.Information($"{nameof(ScheduledStockDataSetupBackgroundService)} number of tickers to process: {allTickersInDb.Count}");

                Task.WaitAll(new Task[] { GetPreviousClosePrices(allTickersInDb, stockService) });

                _logger.Information($"{nameof(ScheduledStockDataSetupBackgroundService)} processed all {allTickersInDb.Count} tickers");
            }
            isTaskActive = false;
        }

        private async Task GetPreviousClosePrices(IList<string> tickers, IStockService stockService, int callLimit = 5)
        {
            for (int i = 0; i < tickers.Count(); i++)
            {
                if (i % callLimit == 0 && i != 0)
                    await Task.Delay(70000);

                _logger.Information("Get ticker previous close price for {@ticker}. Call number {@index} ", tickers[i], i + 1);
                await stockService.GetPreviousClosePrice(tickers[i]);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}