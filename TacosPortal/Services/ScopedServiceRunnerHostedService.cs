//-----------------------------------------------------------------------
// <copyright file="ScopedServiceRunnerHostedService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using TacosPortal.Services.Telegram;

namespace TacosPortal.Services
{
    public class ScopedServiceRunnerHostedService(IServiceProvider serviceProvider, ILogger<ScopedServiceRunnerHostedService> logger) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = serviceProvider.CreateScope())
                        {
                            logger.LogInformation($"Starting ScopedServiceRunnerHostedService Routine Giving all 30 sec to boot");
                            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken).ConfigureAwait(false);
                            var myScopedService = scope.ServiceProvider.GetRequiredService<TelegramWorkerService>();
                            await myScopedService.ExecuteAsync(stoppingToken).ConfigureAwait(false);

                        }
                    }
                    catch (OperationCanceledException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Exception during duplicate check");
                        throw;
                    }

                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in ScopedServiceRunnerHostedService");
            }


        }
    }
}
