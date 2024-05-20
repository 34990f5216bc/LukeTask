using LukeDownloader.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceLayer.PersonInfoDownloadServiceEntities;
using System.Text.Json;

namespace LukeDownloader
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.ConfogureIoc(config);
            ConfigureLogs(builder);

            using IHost host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var myAppService = scope.ServiceProvider.GetRequiredService<IPersonInfoDownloadService>();
                    await myAppService.DownloadPersonData();
                }
                catch (Exception ex) 
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical(ex, "FATAL ERROR");
                }
            }
            await host.StopAsync();
        }

        static void ConfigureLogs(HostApplicationBuilder builder)
        {
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Logging.AddJsonConsole(options =>
            {
                options.JsonWriterOptions = new JsonWriterOptions
                {
                    Indented = true
                };
                options.UseUtcTimestamp = true;
                options.TimestampFormat = "HH:mm:ss.ffffff";
            });
        }
    }
}
