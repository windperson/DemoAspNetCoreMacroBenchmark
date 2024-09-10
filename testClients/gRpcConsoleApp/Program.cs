using EchoGrpc;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Press any key to start...");
Console.ReadKey();
Console.WriteLine("Connecting to gRPC server...\r\n");

// Setup DI for using gRPC client factory
var servicesCollection = new ServiceCollection();
servicesCollection.AddGrpcClient<Echo.EchoClient>(
    options =>
    {
        options.Address = new Uri("https://localhost:7228"); // must align with the grpc server address
    });
    
var serviceProvider = servicesCollection.BuildServiceProvider();

// Get the gRPC client and send a request
var client = serviceProvider.GetRequiredService<EchoGrpc.Echo.EchoClient>();
var response = await client.EchoAsync(new EchoRequest { Message = "Hello from gRPC client!" });

Console.WriteLine($"Response from gRPC server: {response.Message}");
Console.WriteLine("\r\nPress any key to exit...");
Console.ReadKey();
