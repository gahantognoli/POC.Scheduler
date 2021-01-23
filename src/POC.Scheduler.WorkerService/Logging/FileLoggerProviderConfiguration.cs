using Microsoft.Extensions.Logging;

namespace POC.Scheduler.WorkerService.Logging
{
    public class FileLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}
