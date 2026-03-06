//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using LocalGPT;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using Windows.Storage;
using WinRT.Interop;

namespace TacosPortalWin
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private string _baseUrl = string.Empty;
        private WebApplication _webApp;

        private Window _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

        }

        private static async Task WaitForHealthAsync(string baseUrl)
        {
            using var http = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true
            });

            var deadline = DateTime.UtcNow.AddSeconds(10);
            while (DateTime.UtcNow < deadline)
            {
                try
                {
                    var resp = await http.GetAsync($"{baseUrl}/health");
                    if (resp.IsSuccessStatusCode) return;
                }
                catch { }
                await Task.Delay(200);
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            string[] argsi = new string[0];

            var builder = TacosPortal.Program.CreateHostBuilder(argsi).Build();
            await builder.RunAsync();
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            var baseUrl = $"http://localhost:{port}";
            var normalizedBase = baseUrl.Replace("0.0.0.0", "localhost").TrimEnd('/');

            _baseUrl = normalizedBase;

            await WaitForHealthAsync(_baseUrl);

            _window = new TacosPortalWin.MainWindow(_baseUrl);
            _window.Title = "WebView2 Hosts Blazor Backend";
            var appWindow = _window.AppWindow;

            string iconPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "favicon.ico");
            if (File.Exists(iconPath))
            {
                appWindow.SetIcon(iconPath);
            }
            _window.Activate();

            _window.Closed += async (_, __) =>
            {
                if (_webApp != null)
                {
                    await _webApp.StopAsync();
                    await _webApp.DisposeAsync();
                }
            };
        }
    }
}
