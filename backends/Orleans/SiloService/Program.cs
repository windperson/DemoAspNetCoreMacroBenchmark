using System.Net;
using Orleans.Configuration;

namespace SiloService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.UseOrleans(silo =>
        {
            silo.UseLocalhostClustering()
                .Configure<EndpointOptions>(options =>
                {
                    options.AdvertisedIPAddress = IPAddress.Parse("127.0.0.1");
                    options.SiloPort = 11111;
                    options.GatewayPort = 30000;
                })
                .ConfigureLogging(logging => logging.AddConsole());
        });

        var host = builder.Build();
        host.Run();
    }
}