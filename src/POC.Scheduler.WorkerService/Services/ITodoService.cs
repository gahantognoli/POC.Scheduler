using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService.Services
{
    public interface ITodoService 
    {
        Task ObterTodos();
    }
}
