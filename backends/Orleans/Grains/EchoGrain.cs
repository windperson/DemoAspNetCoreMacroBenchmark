using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

// ReSharper disable once UnusedType.Global
public class EchoGrain(ILogger<EchoGrain> logger) : Grain, IEchoGrain
{
    public ValueTask<string> Echo(string message)
    {
        logger.LogInformation("Echoing: {Message}", message);
        return new ValueTask<string>(message);
    }
}