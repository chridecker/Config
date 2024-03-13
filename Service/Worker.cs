using Microsoft.Extensions.Options;
using Service.Settings;

namespace Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<AppSettings> _optionsMonitor;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _logger = logger;
            this._optionsMonitor = optionsMonitor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"{_optionsMonitor.CurrentValue?.Name} running at: {DateTimeOffset.Now}");
                }
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
