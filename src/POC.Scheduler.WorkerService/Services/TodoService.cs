using Hangfire;
using Microsoft.Extensions.Logging;
using POC.Scheduler.WorkerService.Data;
using POC.Scheduler.WorkerService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService.Services
{
    public class TodoService : ServiceBase<TodoService>, ITodoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger<TodoService> _logger;
        private readonly ITodoRepository _todoRepository;

        public TodoService(IHttpClientFactory httpClientFactory, 
            ILogger<TodoService> logger,
            ITodoRepository todoRepository) : base(logger)
        {
            _todoRepository = todoRepository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _httpClient = _httpClientFactory.CreateClient("jsonplaceholder");
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task ObterTodos()
        {
            try
            {
                _logger.LogInformation("**** Obter todos iniciado ****");

                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
                if (!await ValidarResposta(response)) return;

                var todos = await DeserializarRespostaAsync<IEnumerable<Todo>>(response);

                foreach (var todo in todos)
                {
                    await _todoRepository.Adicionar(todo);
                }

                _logger.LogInformation("**** Todos incluidos com sucesso ****");
            }
            catch (Exception e)
            {
                _logger.LogError(@$"**** Falha ao executar job ****\nDetalhes: {e.Message}\n");
            }
        }
    }
}
