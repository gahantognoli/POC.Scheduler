using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POC.Scheduler.WorkerService.Services;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITodoService _todoService;

        public Worker(ILogger<Worker> logger, 
            ITodoService valoresBigService)
        {
            _logger = logger;
            _todoService = valoresBigService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IniciarServidorHangfire();
            AgendarTarefas();

            await Task.CompletedTask;
        }

        private void IniciarServidorHangfire()
        {
            using (new BackgroundJobServer())
            {
                _logger.LogInformation("Hangfire Server iniciado.");
            }
        }

        public void AgendarTarefas()
        {
            //Run At 00:00:00am, every day between 1st and 6th, every month
            RecurringJob.AddOrUpdate("ObterTodos", () =>
                _todoService.ObterTodos(), Cron.Minutely); //cronExpression: "0 0 1-7 1-12 *"
        }
    }
}
