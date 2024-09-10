using BenchmarkDotNet.Attributes;
using GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoRemoteMacroBenchmark.RemoteServiceBenchmarks;

public class OrleansBenchmark : BaseBenchmark
{
    private IHost _host = null!;
    private IClusterClient _clusterClient = null!;
    private IEchoGrain _echoGrain = null!;


    [GlobalSetup]
    public async Task PrepareClusterClient()
    {
        var hostBuilder = Host.CreateDefaultBuilder()
            .UseOrleansClient(client => { client.UseLocalhostClustering(); })
            .ConfigureLogging(logging => logging.ClearProviders())
            .UseConsoleLifetime();
        _host = hostBuilder.Build();
        await _host.StartAsync();
        _clusterClient = _host.Services.GetRequiredService<IClusterClient>();
    }

    [IterationSetup]
    public void ConnectClusterClient()
    {
        _echoGrain = _clusterClient.GetGrain<IEchoGrain>("benchmark");
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetTestData))]
    public async Task<string> Orleans_RPC(RequestMsg request)
    {
        return await _echoGrain.Echo(request.Message);
    }

    [IterationCleanup]
    public void DisconnectClusterClient()
    {
        _echoGrain = null!;
    }

    [GlobalCleanup]
    public async Task CleanupClusterClient()
    {
        _clusterClient = null!;
        await _host.StopAsync();
    }
}