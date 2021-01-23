using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using POC.Scheduler.WorkerService.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService.Data
{
    public class TodoRepository : ITodoRepository, IDisposable
    {
        private readonly SqlConnection connection;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(IConfiguration configuration,
            ILogger<TodoRepository> logger)
        {
            connection = new SqlConnection(configuration.GetConnectionString("TodoDB"));
            _logger = logger;
        }

        public async Task Adicionar(Todo todo)
        {
            var sql = "INSERT INTO Todos VALUES (@titulo, @completa);";
            var sucesso = await connection.ExecuteAsync(sql, new { @titulo = todo.Title, completa = todo.Completed });

            if (sucesso == 0)
                _logger.LogError($"Falha ao inserir o 'todo' {todo.Title} no banco de dados");
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
