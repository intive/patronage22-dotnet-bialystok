using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Patronage.Contracts.Helpers.Reports;
using Patronage.Contracts.Interfaces;
using Patronage.Models;

namespace Patronage.DataAccess.BackgroundServices
{
    public class BackgroundReportWorker : BackgroundService
    {
        private readonly IBackgroundQueue<GenerateReportParams> _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundReportWorker> _logger;

        public BackgroundReportWorker(
            IBackgroundQueue<GenerateReportParams> queue,
            IServiceScopeFactory scopeFactory,
            ILogger<BackgroundReportWorker> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{Type} is now running in the background.", nameof(BackgroundReportWorker));

            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogCritical(
                "The {Type} is stopping due to a host shutdown, queued items might not be processed anymore.",
                nameof(BackgroundReportWorker)
            );

            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //await Task.Delay(500, stoppingToken);
                    var reportParams = _queue.Dequeue();

                    if (reportParams == null) continue;

                    _logger.LogInformation("Starting process of generating report...");

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

                        await reportService.GenerateReportAsync(reportParams, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("An error occurred. Exception: {@Exception}", ex);
                }
            }
        }
    }
}