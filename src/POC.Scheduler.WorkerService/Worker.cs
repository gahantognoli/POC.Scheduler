using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POC.Scheduler.WorkerService.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScheduler _scheduler;

        public Worker(ILogger<Worker> logger, 
            IScheduler scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IniciarServidorHangfire();
            _scheduler.AgendarTarefas();

            await Task.CompletedTask;
        }

        private BackgroundJobServer IniciarServidorHangfire() => new BackgroundJobServer();
    }
}
