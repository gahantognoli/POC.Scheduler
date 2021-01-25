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
        private readonly ILogger<TodoRepository> _logger;
        private readonly IConfiguration _configuration;

        private SqlConnection connection;

        public TodoRepository(IConfiguration configuration,
            ILogger<TodoRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Adicionar(Todo todo)
        {
            Conectar();

            var sql = "INSERT INTO Todos VALUES (@titulo, @completa);";
            var sucesso = await connection.ExecuteAsync(sql, new { @titulo = todo.Title, completa = todo.Completed });

            if (sucesso == 0)
                _logger.LogError($"Falha ao inserir o 'todo' {todo.Title} no banco de dados");

            connection?.Dispose();
        }

        private void Conectar() => connection = new SqlConnection(_configuration.GetConnectionString("TodoDB"));

        public void Dispose() => connection?.Dispose();
    }
}
