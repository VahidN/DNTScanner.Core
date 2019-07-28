using System;
using System.Threading.Tasks;
using DNTScanner.Core;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace DNTScanner.WindowsService
{
    public interface IScannerHubClient
    {
        Task CallGetErrors(string error);
        Task CallGetScannerSettings(ScannerSettings firstScanner);
        void RegisterCallbacks(Action<string> detectScanner, Action<NewScanConfig> doScan);
        Task CallScannerIsNotConnectedError();
        Task StartAsync();
        Task StopAsync();
    }

    public class ScannerHubClient : IScannerHubClient
    {
        private const string GetErrors = "GetErrors";
        private const string DoScan = "DoScan";
        private const string DetectScanner = "DetectScanner";
        private const string GetScannerSettings = "GetScannerSettings";

        private readonly HubConnection _hubConnection;
        private readonly ILogger<ScannerHubClient> _logger;

        public ScannerHubClient(IOptions<AppConfig> appConfig, ILogger<ScannerHubClient> logger)
        {
            var settings = appConfig.Value;
            _hubConnection = new HubConnectionBuilder()
                                    .WithUrl(new Uri(new Uri(settings.BaseAddress), settings.HubPath),
                                        options => options.Headers["AppId"] = settings.AppId)
                                    //.ConfigureLogging(loggingBuilder => loggingBuilder.AddDebug().AddConsole())
                                    .Build();
            _logger = logger;
        }

        public void RegisterCallbacks(Action<string> detectScanner, Action<NewScanConfig> doScan)
        {
            _hubConnection.On<string>(DetectScanner, detectScanner);
            _hubConnection.On<NewScanConfig>(DoScan, doScan);

            _hubConnection.Closed += onClosed;
        }

        private async Task onClosed(Exception error)
        {
            try
            {
                _logger.LogError(error, "Connection closed.");

                await Task.Delay(new Random().Next(0, 10) * 1000);
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection closed.");
            }
        }

        public Task StartAsync()
        {
            return _hubConnection.StartAsync();
        }

        public Task StopAsync()
        {
            _hubConnection.Closed -= onClosed;
            return _hubConnection.DisposeAsync();
        }

        public Task CallScannerIsNotConnectedError()
        {
            const string error = "Please connect your scanner to the system and also make sure its driver is installed.";
            _logger.LogError(error);
            return _hubConnection.InvokeAsync(GetErrors, error);
        }

        public Task CallGetScannerSettings(ScannerSettings firstScanner)
        {
            return _hubConnection.InvokeAsync(GetScannerSettings, firstScanner);
        }

        public async Task CallGetErrors(string error)
        {
            try
            {
                await _hubConnection.InvokeAsync(GetErrors, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending messages to the server.");
            }
        }
    }
}