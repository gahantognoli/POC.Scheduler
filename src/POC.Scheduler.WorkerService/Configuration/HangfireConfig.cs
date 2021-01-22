using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POC.Scheduler.WorkerService.Configuration
{
    public static class HangfireConfig
    {
        public static void AddHangfireConfiguration(this IServiceCollection services, 
            IConfiguration settings)
        {
            services.AddHangfire(configuration =>
            {
                configuration.UseSqlServerStorage(settings.GetConnectionString("HangfireDB"));
            });
            services.AddHangfireServer();
        }
    }
}
