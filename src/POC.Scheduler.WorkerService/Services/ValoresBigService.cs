using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService.Services
{
    public class ValoresBigService : IValoresBigService
    {
        private readonly HttpClient _httpClient;

        public ValoresBigService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://teste.com.br");
            _httpClient = httpClient;
        }

        public async Task ObterValoresCobranca()
        {
            Thread.Sleep(100000);

            Console.WriteLine("Terminou - Obtendo valores Big");

            await Task.CompletedTask;
        }
    }
}
