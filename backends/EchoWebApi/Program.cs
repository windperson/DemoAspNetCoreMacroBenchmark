namespace EchoWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
