namespace SiloService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.UseOrleans(silo =>
        {
            silo.UseLocalhostClustering()
                .ConfigureLogging(logging => logging.AddConsole());
        });

        var host = builder.Build();
        host.Run();
    }
}