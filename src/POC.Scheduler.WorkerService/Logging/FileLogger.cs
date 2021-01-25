using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace POC.Scheduler.WorkerService.Logging
{
    public class FileLogger : ILogger
    {
        readonly string loggerName;
        readonly FileLoggerProviderConfiguration loggerConfig;

        public FileLogger(string loggerName, FileLoggerProviderConfiguration loggerConfig)
        {
            this.loggerName = loggerName;
            this.loggerConfig = loggerConfig;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if((int)logLevel >= (int)loggerConfig.LogLevel)
            {
                string mensagem = string.Format("{0}: {1} - {2}", logLevel.ToString(), eventId.Id, formatter(state, exception));
                EscreverTextoNoArquivo(mensagem);
            }
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivo = "c:\\temp\\log_worker.txt";
            if (File.Exists(caminhoArquivo))
            {
                using (StreamWriter streamWritter = new StreamWriter(caminhoArquivo, true))
                {
                    streamWritter.WriteLine(mensagem);
                    streamWritter.Close();
                }
            }
        }
    }
}
