using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Patronage.DataAccess.BackgroundServices
{
    public class BackgroundReportWorker : BackgroundService
    {
        private readonly IBackgroundQueue<Report> _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundReportWorker> _logger;

        public BackgroundReportWorker(
            IBackgroundQueue<Report> queue,
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
                    await Task.Delay(500, stoppingToken);
                    var report = _queue.Dequeue();

                    if (report == null) continue;

                    _logger.LogInformation("Starting to process ..");

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        // TODO: Here you should call a service producing report i think

                        //var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

                        //await reportService.CreateReportAsync(report, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("An error occurred when publishing a book. Exception: {@Exception}", ex);
                }
            }
        }

        // Mock database table
        public class Report
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Status { get; set; }
            public string File { get; set; }
        }
    }
}