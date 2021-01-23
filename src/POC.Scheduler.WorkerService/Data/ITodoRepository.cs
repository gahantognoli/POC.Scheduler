using POC.Scheduler.WorkerService.Models;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService.Data
{
    public interface ITodoRepository
    {
        Task Adicionar(Todo todo);
    }
}
