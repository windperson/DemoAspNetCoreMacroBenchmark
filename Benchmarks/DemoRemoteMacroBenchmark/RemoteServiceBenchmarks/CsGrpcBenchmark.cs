using System.Threading.Channels;
using BenchmarkDotNet.Attributes;
using EchoGrpc;
using Microsoft.Extensions.DependencyInjection;

namespace DemoRemoteMacroBenchmark.RemoteServiceBenchmarks;

public class CsGrpcBenchmark : BaseBenchmark
{
    private ServiceProvider _serviceProvider = null!;

    private Echo.EchoClient _client = null!;

    [GlobalSetup]
    public void PrepareClient()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddGrpcClient<Echo.EchoClient>(
            options =>
            {
                options.Address = new Uri("https://localhost:7228");
                options.ChannelOptionsActions.Add(channelOptions =>
                {
                    // you need to raise the message size limit to send large messages
                    // see https://github.com/grpc/grpc-dotnet/issues/2277#issuecomment-1728559455
                    channelOptions.MaxSendMessageSize = int.MaxValue;
                    channelOptions.MaxReceiveMessageSize = int.MaxValue;
                });
            });
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [IterationSetup]
    public void InitGrpcClient()
    {
        _client = _serviceProvider.GetRequiredService<Echo.EchoClient>();
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetTestData))]
    public async Task<string> gRPC_Invoke(RequestMsg request)
    {
        var reply = await _client.EchoAsync(new EchoRequest { Message = request.Message });
        return reply.Message;
    }

    [IterationCleanup]
    public void CleanupClient()
    {
        _client = null!;
    }

    [GlobalCleanup]
    public void CleanupServiceProvider()
    {
        _serviceProvider.Dispose();
    }
}