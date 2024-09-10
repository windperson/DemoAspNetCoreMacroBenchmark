using System.Net.Http.Json;
using BenchmarkDotNet.Attributes;
using DemoRemoteMacroBenchmark.GenerateTestData;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DemoRemoteMacroBenchmark.RemoteServiceBenchmarks;

// ReSharper disable once InconsistentNaming
public class RESTApiBenchmark : BaseBenchmark
{
    private const string HttpClientName = "Benchmark";
    private IHttpClientFactory _httpClientFactory = null!;
    private HttpClient _httpClient = null!;

    private ServiceProvider _serviceProvider = null!;

    [GlobalSetup]
    public void PrepareHttpClientFactory()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddHttpClient(HttpClientName, client =>
        {
            client.BaseAddress = new Uri("https://localhost:7200");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    [IterationSetup]
    public void CreateHttpClient()
    {
        _httpClient = _httpClientFactory.CreateClient(HttpClientName);
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetTestData))]
    public async Task<string> REST_WebAPI(RequestMsg request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/echo", request);
        response.EnsureSuccessStatusCode();
        var resultObj = await response.Content.ReadFromJsonAsync<ResponseMsg>() ??
                        throw new JsonSerializationException("Fetch echo message from Web API failed");
        return resultObj.Echo;
    }

    [IterationCleanup]
    public void DisposeHttpClient()
    {
        _httpClient.Dispose();
    }

    [GlobalCleanup]
    public void CleanupAllService()
    {
        _serviceProvider.Dispose();
    }
}