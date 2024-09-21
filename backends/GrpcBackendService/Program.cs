
using GrpcBackendService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(gRpcOption =>
{
    // You need to raise the message size limit to allow large messages to be sent and received
    // see https://github.com/grpc/grpc-dotnet/issues/2277#issuecomment-1728559455
    gRpcOption.MaxReceiveMessageSize = int.MaxValue;
    gRpcOption.MaxSendMessageSize = int.MaxValue;
});

// Configure Kestrel server options
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Set to unlimited for by pass the default KestrelServerLimits.MaxRequestBodySize 
    // Defaults to 30,000,000 bytes, which is approximately 28.6MB. 
    // https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.server.kestrel.core.kestrelserverlimits.maxrequestbodysize?view=aspnetcore-8.0
    serverOptions.Limits.MaxRequestBodySize = null; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<EchoService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();