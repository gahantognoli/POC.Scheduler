using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace POC.Scheduler.WorkerService.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        readonly FileLoggerProviderConfiguration loggerConfig;
        readonly ConcurrentDictionary<string, FileLogger> loggers = new ConcurrentDictionary<string, FileLogger>();

        public FileLoggerProvider(FileLoggerProviderConfiguration config)
        {
            loggerConfig = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new FileLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            //
        }
    }
}
