using EchoGrpc;
using Grpc.Core;

namespace GrpcBackendService.Services;

public class EchoService(ILogger<EchoService> logger) : Echo.EchoBase
{
    public override Task<EchoResponse> Echo(EchoRequest request, ServerCallContext context)
    {
        logger.LogInformation("Echoing: {Message}", request.Message);
        return Task.FromResult(new EchoResponse
        {
            Message = request.Message
        });
    }
}