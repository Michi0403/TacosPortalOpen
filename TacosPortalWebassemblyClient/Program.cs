//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net;
using System.Net.Http.Headers;
using TacosPortalWebassemblyClient.Interfaces;
using TacosPortalWebassemblyClient.Services;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TacosPortalWebassemblyClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ServicePointManager.Expect100Continue = false;
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            _ = builder.Services.AddLogging(loggingBuilder =>
            {


                _ = loggingBuilder.AddDebug();
                _ = loggingBuilder.AddFilter("Microsoft", LogLevel.Debug);
                _ = loggingBuilder.AddFilter("System", LogLevel.Debug);
                _ = loggingBuilder.AddFilter("TacosPortalWebassemblyClient", LogLevel.Debug);
            });
            _ = builder.Logging.SetMinimumLevel(LogLevel.Debug);

            _ = builder.Services.AddDevExpressBlazor(options =>
            {
                options.BootstrapVersion = BootstrapVersion.v5;
            });
            _ = builder.Services.AddCascadingAuthenticationState();
            _ = builder.Services.AddScoped<WebAPIAuthenticationStateProvider>();
            _ = builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());
            _ = builder.Services.AddScoped<AuthenticationStateProvider, WebAPIAuthenticationStateProvider>();
            _ = builder.Services.AddScoped<INotificationService, WebassemblyClientNotificationService>();
            _ = builder.Services.AddApiAuthorization();
            _ = builder.Services.AddTransient<CookieHandlerWASM>();

            _ = builder.Services.AddHttpClient("WasmClient")
            .AddHttpMessageHandler<CookieHandlerWASM>();

            _ = builder.Services.AddScoped(sp =>
            {

                var factory = sp.GetService<IHttpClientFactory>();
                if (factory is not null)
                {
                    var client = factory.CreateClient("WasmClient");
                    client.Timeout = TimeSpan.FromSeconds(120);
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                    _ = client.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                    _ = client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                    return client;
                }


                var env = sp.GetRequiredService<IWebAssemblyHostEnvironment>();
                var handler = sp.GetRequiredService<CookieHandlerWASM>();
                var newClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(env.BaseAddress, UriKind.Absolute)
                };
                newClient.Timeout = TimeSpan.FromSeconds(120);
                newClient.DefaultRequestHeaders.ExpectContinue = false;
                newClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                newClient.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                newClient.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                newClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                newClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                _ = newClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                newClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                newClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                _ = newClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                return newClient;
            });


            _ = builder.Services.ConfigureHttpClientDefaults(http =>
            {


                if (builder.HostEnvironment.BaseAddress != null)
                {
                    _ = http.ConfigureHttpClient(client =>
                    {
                        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                        client.Timeout = TimeSpan.FromSeconds(120);
                        client.DefaultRequestHeaders.ExpectContinue = false;
                        client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                        client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                        client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                        _ = client.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                        _ = client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                    });

                }
                else
                {
                    _ = http.ConfigureHttpClient((sp, client) =>
                    {
                        var env = sp.GetRequiredService<IWebAssemblyHostEnvironment>();
                        client.BaseAddress = new Uri(env.BaseAddress);
                        client.Timeout = TimeSpan.FromSeconds(120);
                        client.DefaultRequestHeaders.ExpectContinue = false;
                        client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                        client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                        client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        _ = client.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                        _ = client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                    });
                }

                _ = http.AddHttpMessageHandler<CookieHandlerWASM>();

                _ = http.AddDefaultLogger();
            });

            var host = builder.Build();


            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Blazor WASM client starting...");
            await host.RunAsync().ConfigureAwait(false);
        }
    }
}
