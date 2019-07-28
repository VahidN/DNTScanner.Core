using System;
using System.Threading.Tasks;
using DNTScanner.Core;
using Microsoft.AspNetCore.SignalR;

namespace DNTScanner.ASPNETCoreApp.Hubs
{
    public class ScannerHub : Hub
    {
        public Task GetScannerSettings(ScannerSettings scannerSettings)
        {
            return Clients.All.SendAsync("OnScannerSettingsReceived", scannerSettings);
        }

        public Task GetErrors(string error)
        {
            return Clients.All.SendAsync("OnErrorsReceived", error);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Clients.All.SendAsync("OnClientDisconnected", getAppId());
        }

        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("OnClientConnected", getAppId());
        }

        private string getAppId()
        {
            var httpCtx = Context.GetHttpContext();
            return httpCtx.Request.Headers["AppId"].ToString();
        }
    }
}