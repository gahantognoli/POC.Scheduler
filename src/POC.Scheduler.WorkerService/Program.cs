using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POC.Scheduler.WorkerService.Configuration;
using POC.Scheduler.WorkerService.Data;
using POC.Scheduler.WorkerService.Logging;
using POC.Scheduler.WorkerService.Services;

namespace POC.Scheduler.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(config =>
                {
                    config.ClearProviders();
                    config.AddConsole();
                    config.AddProvider(new FileLoggerProvider(new FileLoggerProviderConfiguration()
                    {
                        LogLevel = LogLevel.Warning,
                    }));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddHttpClient();
                    services.AddSingleton<ITodoRepository, TodoRepository>();
                    services.AddSingleton<ITodoService, TodoService>();
                    services.AddHangfireConfiguration(hostContext.Configuration);
                });
    }
}
