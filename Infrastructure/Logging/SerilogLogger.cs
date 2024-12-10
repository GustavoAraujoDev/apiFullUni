using Serilog;
using Serilog.Settings.Configuration;
using System;


namespace apiFullUni.Infrastructure.Logging
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger _logger;

        public SerilogLogger()
        {
            // Configuração do logger usando appsettings.json
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build())
                .CreateLogger();
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }
    }
}
