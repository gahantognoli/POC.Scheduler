using Hangfire;
using Hangfire.Storage;
using POC.Scheduler.WorkerService.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace POC.Scheduler.WorkerService
{
    public interface IScheduler
    {
        void AgendarTarefas();
    }

    public class Scheduler : IScheduler
    {
        private readonly IValoresBigService _valoresBigService;

        public Scheduler(IValoresBigService valoresBigService)
        {
            _valoresBigService = valoresBigService;
        }

        public void AgendarTarefas()
        {
            //Run At 00:00:00am, every day between 1st and 6th, every month
            RecurringJob.AddOrUpdate("ObterValoresBig", () =>
                _valoresBigService.ObterValoresCobranca(), Cron.Minutely); //cronExpression: "0 0 1-7 1-12 *"
        }
    }
}
