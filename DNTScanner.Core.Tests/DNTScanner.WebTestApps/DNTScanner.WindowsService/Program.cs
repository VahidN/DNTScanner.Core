using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DNTScanner.WindowsService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var serviceProvider = ApplicationServices.ServiceProvider)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                try
                {
                    var scannerHubClient = serviceProvider.GetRequiredService<IScannerHubClient>();
                    var scannerService = serviceProvider.GetRequiredService<IScannerService>();

                    scannerHubClient.RegisterCallbacks(
                        async (msg) => await scannerService.DetectScanner(msg),
                        async (newScanConfig) => await scannerService.DoScan(newScanConfig));

                    await scannerHubClient.StartAsync();
                    logger.LogInformation("Connection started.");

                    logger.LogInformation("Press a key to terminate the application ...");
                    Console.ReadKey();

                    await scannerHubClient.StopAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Program terminated.");
                }
            }
        }
    }
}