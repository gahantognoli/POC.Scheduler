using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace POC.Scheduler.WorkerService.Services
{
    public abstract class ServiceBase<T>
    {
        private readonly ILogger<T> _logger;
        public ServiceBase(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected async Task<T> DeserializarRespostaAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            var serializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<T>(await httpResponseMessage.Content.ReadAsStreamAsync(), serializationOptions);
        }

        protected async Task<bool> ValidarResposta(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode) return true;

            if (string.IsNullOrEmpty(await httpResponseMessage.Content.ReadAsStringAsync()))
            {
                _logger.LogError("Nenhum dado retornado");
                return false;
            }

            switch ((int)httpResponseMessage.StatusCode)
            {
                case 400:
                    _logger.LogError("Dados passados ao web service está incorreto");
                    break;
                case 404:
                    _logger.LogError("URL do web serviço não foi encontrada");
                    break;
                case 500:
                    _logger.LogError("Falha interna do web service");
                    break;
                default:
                    _logger.LogError("Erro ainda não tratado");
                    break;
            }

            return false;
        }
    }
}
