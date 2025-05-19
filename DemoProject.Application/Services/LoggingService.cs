using System.Threading.Channels;

namespace DemoProject.Application.Services;

public class LoggingService
{
    private readonly Channel<string> logChannel = Channel.CreateUnbounded<string>();

    public async Task LogAsync(string message)
    {
        await logChannel.Writer.WriteAsync(message);
    }

    public async Task ProcessLogs(CancellationToken stoppingToken)
    {
        await foreach (var logMessage in logChannel.Reader.ReadAllAsync(stoppingToken))
        {
            Console.WriteLine($"Log: {logMessage}");
        }
    }
}