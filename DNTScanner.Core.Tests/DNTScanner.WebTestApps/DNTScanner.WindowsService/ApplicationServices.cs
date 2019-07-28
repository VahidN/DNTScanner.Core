using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DNTScanner.WindowsService
{
    /// <summary>
    /// A lazy loaded thread-safe singleton App ServiceProvider.
    /// </summary>
    public static class ApplicationServices
    {
        private static readonly Lazy<ServiceProvider> _serviceProviderBuilder =
                    new Lazy<ServiceProvider>(getServiceProvider, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Defines a mechanism for retrieving a service object.
        /// </summary>
        public static ServiceProvider ServiceProvider { get; } = _serviceProviderBuilder.Value;

        private static ServiceProvider getServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            configureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private static void configureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
            var baseAddress = config["DNTScannerConfig:BaseAddress"];
            services.AddHttpClient<UploadApiClient>(x => x.BaseAddress = new Uri(baseAddress));
            services.Configure<AppConfig>(options => config.GetSection("DNTScannerConfig").Bind(options));
            services.AddLogging(cfg => cfg.AddConsole().AddDebug());

            services.AddSingleton<IScannerHubClient, ScannerHubClient>();
            services.AddSingleton<IScannerService, ScannerService>();
        }
    }
}