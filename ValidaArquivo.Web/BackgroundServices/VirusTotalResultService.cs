using ValidaArquivo.Domain.Services;

namespace ValidaArquivo.Web.BackgroundServices
{
    /// <summary>
    /// Serviço em segundo plano para análise e envio dos arquivos para o virus total
    /// - Executado em intervalos de 5 minutos
    /// </summary>
    public class VirusTotalResultService : BackgroundService
    {
        private readonly ILogger<VirusTotalResultService> _logger;
        private readonly ArquivoUploadService _arquivoUploadService;

        public VirusTotalResultService(
            ILogger<VirusTotalResultService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _arquivoUploadService = factory.CreateScope().ServiceProvider.GetRequiredService<ArquivoUploadService>();
        }

        /// <summary>
        /// Execução das tarefas:
        ///     - Buscar resultados prontos das análises
        ///     - Reenviar arquivos com erro ou pendentes
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Starting updater service");
            stoppingToken.Register(() => _logger.LogDebug("Updater service is stopping."));
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Generating pdf in background");

                await _arquivoUploadService.BuscarResultados();
                await _arquivoUploadService.ReenviarPendentes();
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
            _logger.LogDebug("Pdf generated");
        }
    }
}
