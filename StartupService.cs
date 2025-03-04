using WebFerma.WorkWithHub;

namespace WebFerma
{
    public class StartupService : IHostedService
    {
        private readonly IHubConnection _hubConnection;
        private readonly ILogger<StartupService> _logger;

        public StartupService(IHubConnection hubConnection, ILogger<StartupService> logger)
        {
            _hubConnection = hubConnection;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запуск сервиса при старте приложения...");

            try
            {
                var microservices = await _hubConnection.GetMicroservices();
                foreach (var microservice in microservices)
                {
                    _logger.LogInformation($"Microservice: {microservice.Name}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при получении микросервисов: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Сервис остановлен.");
            return Task.CompletedTask;
        }
    }
}
