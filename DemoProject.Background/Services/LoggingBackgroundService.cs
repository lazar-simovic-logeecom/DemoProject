using DemoProject.Application.Services;
using Microsoft.Extensions.Hosting;

namespace DemoProject.Background.Services;

public class LoggingBackgroundService(LoggingService loggingService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await loggingService.ReadAsync(stoppingToken);

            if (message != null)
            {
                Console.WriteLine($"[LOG] {message}");
            }
        }
    }
}