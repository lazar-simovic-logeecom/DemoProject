using System.Threading.Channels;

namespace DemoProject.Application.Services;

public class LoggingService
{
    private readonly Channel<string> _logChannel;

    public LoggingService(Channel<string> logChannel)
    {
        _logChannel = logChannel;
    }

    public async Task LogAsync(string message)
    {
        await _logChannel.Writer.WriteAsync(message);
    }
}