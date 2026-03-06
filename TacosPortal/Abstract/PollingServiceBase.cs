//-----------------------------------------------------------------------
// <copyright file="PollingServiceBase.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using TacosPortal.Interfaces;

namespace TacosPortal.Abstract;




public abstract class PollingServiceBase<TReceiverService>(IServiceProvider serviceProvider, ILogger<PollingServiceBase<TReceiverService>> logger)
    : BackgroundService where TReceiverService : IReceiverService
{

    private async Task DoWork(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {


                using var scope = serviceProvider.CreateScope();
                var receiver = scope.ServiceProvider.GetRequiredService<TReceiverService>();

                await receiver.ReceiveAsync(stoppingToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Polling failed with exception: {Exception}");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken).ConfigureAwait(false);
                throw;
            }
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Starting polling service");
            await DoWork(stoppingToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ExecuteAsync failed with exception: {Exception}");
            throw;
        }

    }
}
