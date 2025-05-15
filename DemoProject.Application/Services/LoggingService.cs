using System.Threading.Channels;

namespace DemoProject.Application.Services;

public class LoggingService(Channel<string> logChannel)
{
    public async Task LogAsync(string message)
    {
        await logChannel.Writer.WriteAsync(message);
    }
}