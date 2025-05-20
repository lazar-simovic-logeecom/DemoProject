using System.Threading.Channels;

namespace DemoProject.Application.Services;

public class LoggingService
{
    private readonly Channel<string> logChannel = Channel.CreateUnbounded<string>();

    public async Task LogAsync(string message)
    {
        await logChannel.Writer.WriteAsync(message);
    }

    public async Task<string?> ReadAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await logChannel.Reader.ReadAsync(cancellationToken);
        }
        catch (ChannelClosedException)
        {
            return null;
        }
    }
}