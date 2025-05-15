using System.Threading.Channels;
using DemoProject.Application.Services;
using Microsoft.Extensions.Hosting;

namespace DemoProject.Background;

public class LoggingBackgroundService(Channel<string> logChannel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await foreach (var logMessage in logChannel.Reader.ReadAllAsync(stoppingToken))
            {
                Console.WriteLine($"Log: {logMessage}");
            }
        }
    }
}