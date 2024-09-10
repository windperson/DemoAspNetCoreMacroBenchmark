using GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client => { client.UseLocalhostClustering(); })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

using var host = hostBuilder.Build();
Console.WriteLine("Press any key to start client...");
Console.ReadKey();
await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

var echoGrain = client.GetGrain<IEchoGrain>("demo");
var response = await echoGrain.Echo("Hello, World!");

Console.WriteLine("\r\nEchoGrain Response:{0}\r\n", response);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

await host.StopAsync();