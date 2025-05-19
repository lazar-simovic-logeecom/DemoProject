using System.Threading.Channels;
using DemoProject.Application.Services;
using Microsoft.Extensions.Hosting;

namespace DemoProject.Background;

public class LoggingBackgroundService(LoggingService loggingService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await loggingService.ProcessLogs(stoppingToken);
        }
    }
}